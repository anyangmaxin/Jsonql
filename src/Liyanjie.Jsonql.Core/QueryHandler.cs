using Liyanjie.Jsonql.Core.Internals;
using Liyanjie.Jsonql.Core.Parsers;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liyanjie.Jsonql.Core
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class QueryHandler : IDisposable
    {
        readonly ResourceTable resourceTable;
        readonly IDynamicEvaluator dynamicEvaluator;
        readonly IDynamicLinq dynamicLinq;

        IDictionary<string, Resource> resources;
        IDictionary<string, string> expressions;
        IDictionary<string, string> templates;
        string entry;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceTable"></param>
        /// <param name="dynamicEvaluator"></param>
        /// <param name="dynamicLinq"></param>
        public QueryHandler(ResourceTable resourceTable,
            IDynamicEvaluator dynamicEvaluator,
            IDynamicLinq dynamicLinq)
        {
            if (resourceTable == null)
                throw new ArgumentNullException(nameof(resourceTable));

            this.resourceTable = resourceTable;
            this.dynamicEvaluator = dynamicEvaluator;
            this.dynamicLinq = dynamicLinq;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            resources?.Clear();
            expressions?.Clear();
            templates?.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="authorization"></param>
        /// <returns></returns>
        public async Task<string> Handle(string query, IAuthorization authorization)
        {
            using (var queryParser = new QueryParser(query))
            {
                resources = queryParser.Resources.ToDictionary(_ => _.Key, _ => resourceTable.GetResource(_.Value, authorization, dynamicLinq));
                expressions = queryParser.Expressions.Copy();
                templates = queryParser.Templates.Copy();
                entry = queryParser.Entry;
            }
            var @object = await createObject(templates[entry]);
            return JsonConvert.SerializeObject(@object);
        }

        async Task<IDictionary<string, object>> createObject(string template, IDictionary<string, object> variables = null, object @object = null)
        {
            var properties = template.TrimStart('{').TrimEnd('}').Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            var @return = new Dictionary<string, object>();
            var tmpVariables = variables.Copy();
            foreach (var property in properties)
            {
                var segments = property.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                var name = segments[0];
                var valueTemplate = segments.Length > 1 ? segments[1] : $"$.{name}";
                if (name.StartsWith("$"))
                {
                    var value = await getValue1(valueTemplate, tmpVariables, true, @object);
                    if (tmpVariables.ContainsKey(name))
                        tmpVariables[name] = value;//覆盖变量
                    else
                        tmpVariables.Add(name, value);
                }
                else
                {
                    var value = await getValue1(valueTemplate, tmpVariables, false, @object);
                    @return.Add(name, value);
                }
            }

            return @return;
        }

        async Task<object> getValue1(string template, IDictionary<string, object> variables, bool isVariable, object @object = null)
        {
            //枚举
            if (template.IndexOf("=>") > 0)
            {
                var segments = template.Split(new[] { "=>" }, 2, StringSplitOptions.RemoveEmptyEntries);

                var valueTemplate = segments[0];
                var enumerateTemplate = segments[1];

                var value = await getValue2(valueTemplate, variables, @object);
                var queryable = value is Resource
                    ? (value as Resource)?.Queryable
                    : (value as IEnumerable)?.AsQueryable();
                if (queryable == null)
                    return null;

                var isArrayReturn = enumerateTemplate.EndsWith("[]");
                if (isArrayReturn)
                    enumerateTemplate = enumerateTemplate.Substring(0, enumerateTemplate.Length - 2);

                if (dynamicLinq != null)
                {
                    var selector = new SelectParser(templates).Parse(enumerateTemplate);
                    if (!string.IsNullOrWhiteSpace(selector))
                        queryable = dynamicLinq.Select(queryable, selector, new object[0]);
                }

                if (isArrayReturn)
                {
                    var list = new List<object>();
                    var source = dynamicLinq == null ? QueryableHelper.ToList(queryable) : dynamicLinq.ToList(queryable);
                    foreach (var item in source)
                    {
                        list.Add(await getValue1(enumerateTemplate, variables, false, item));
                    }
                    return list;
                }
                else
                {
                    var first = dynamicLinq == null
                        ? QueryableHelper.FirstOrDefault(queryable)
                        : dynamicLinq.FirstOrDefault(queryable);
                    if (first == null)
                        return null;

                    return await getValue1(enumerateTemplate, variables, false, first);
                }
            }
            //取值
            else
            {
                var value = await getValue2(template, variables, @object);
                if (value is Resource)
                {
                    if (isVariable)
                        return value;
                    else
                    {
                        var queryable = (value as Resource).Queryable;
                        return dynamicLinq == null
                            ? QueryableHelper.ToList(queryable)
                            : dynamicLinq.ToList(queryable);
                    }
                }
                return value;
            }
        }

        async Task<object> getValue2(string template, IDictionary<string, object> variables, object @object = null)
        {
            if ("$".Equals(template))//对象自身
                return @object;
            else if (template.StartsWith("$."))//对象的属性或方法
                return @object.GetValue(template.Substring(2));
            else if (template.StartsWith("`"))//表达式
                return evaluateExpression(expressions[template], variables);
            else if (template.StartsWith("#"))//模板
                return await createObject(templates[template], variables, @object);
            else if (template.StartsWith("@"))//资源
                return createResource(template, variables);
            else if (template.StartsWith("$"))//变量
                return createResource(template, variables);
            else
                return JsonConvert.DeserializeObject(template);
        }

        object createResource(string template, IDictionary<string, object> variables)
        {
            object @return = null;

            string[] segments = null;
            using (var accessParser = new AccessParser(template))
            {
                segments = accessParser.Parse();
            }

            var segment0 = segments[0];
            if (segment0.StartsWith("@"))
                @return = resources[segment0];
            else if (segment0.StartsWith("$"))
                @return = variables[segment0];

            for (var i = 1; i < segments.Length; i++)
            {
                var segment = segments[i];
                if (@return is Resource)
                {
                    var method = new MethodParser(segment).Parse();
                    var parameters = method.Value.Value?.Select(_ => variables[_]).ToArray();
                    var resource = (@return as Resource).SetParameters(parameters);
                    switch (method.Key.ToLower())
                    {
                        case "all":
                            @return = resource.All(method.Value.Key);
                            break;
                        case "any":
                            @return = string.IsNullOrEmpty(method.Value.Key)
                                ? resource.Any()
                                : resource.Any(method.Value.Key);
                            break;
                        case "average":
                            @return = resource.Average(method.Value.Key);
                            break;
                        case "count":
                            @return = string.IsNullOrEmpty(method.Value.Key)
                                ? resource.Count()
                                : resource.Count(method.Value.Key);
                            break;
                        case "distinct":
                            @return = resource.Distinct();
                            break;
                        case "groupby":
                            @return = resource.GroupBy(method.Value.Key);
                            break;
                        case "max":
                            @return = resource.Max(method.Value.Key);
                            break;
                        case "min":
                            @return = resource.Min(method.Value.Key);
                            break;
                        case "orderby":
                            @return = resource.OrderBy(method.Value.Key);
                            break;
                        case "orderbydescending":
                            @return = resource.OrderByDescending(method.Value.Key);
                            break;
                        case "select":
                            @return = resource.Select(method.Value.Key);
                            break;
                        case "skip":
                            @return = resource.Skip(int.Parse(method.Value.Key));
                            break;
                        case "sum":
                            @return = resource.Sum(method.Value.Key);
                            break;
                        case "take":
                            @return = resource.Take(int.Parse(method.Value.Key));
                            break;
                        case "where":
                            @return = resource.Where(method.Value.Key);
                            break;
                        case "thenby":
                            @return = (resource as Resource_Ordered).ThenBy(method.Value.Key);
                            break;
                        case "thenbydescending":
                            @return = (resource as Resource_Ordered).ThenByDescending(method.Value.Key);
                            break;
                        default:
                            break;
                    }
                }
                else
                    throw new Exception($"无法对非资源对象调用 {segment} 方法");
            }

            return @return;
        }

        object evaluateExpression(string expression, IDictionary<string, object> variables)
        {
            return dynamicEvaluator.Evaluate(expression.TrimStart('{').TrimEnd('}'), variables);
        }
    }
}
