using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Liyanjie.Jsonql.Core.Internals;
using Liyanjie.Jsonql.Core.Parsers;
using Newtonsoft.Json;

namespace Liyanjie.Jsonql.Core
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class QueryHandler : IDisposable
    {
        readonly ResourceTable resourceTable;
        readonly IJsonqlIncluder jsonqlIncluder;
        readonly IJsonqlLinqer jsonqlLinqer;
        readonly IJsonqlEvaluator jsonqlEvaluator;

        IDictionary<string, Resource> resources;
        IDictionary<string, string> expressions;
        IDictionary<string, string> templates;
        string entry;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceTable"></param>
        /// <param name="jsonqlIncluder"></param>
        /// <param name="jsonqlLinqer"></param>
        /// <param name="jsonqlEvaluator"></param>
        public QueryHandler(ResourceTable resourceTable,
            IJsonqlIncluder jsonqlIncluder,
            IJsonqlLinqer jsonqlLinqer,
            IJsonqlEvaluator jsonqlEvaluator)
        {
            this.resourceTable = resourceTable ?? throw new ArgumentNullException(nameof(resourceTable));
            this.jsonqlIncluder = jsonqlIncluder;
            this.jsonqlLinqer = jsonqlLinqer;
            this.jsonqlEvaluator = jsonqlEvaluator;
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
        /// <param name="jsonqlAuthorization"></param>
        /// <returns></returns>
        public async Task<string> Handle(string query, IJsonqlAuthorization jsonqlAuthorization)
        {
            using (var queryParser = new QueryParser(query))
            {
                resources = queryParser.Resources.ToDictionary(_ => _.Key, _ => resourceTable.GetResource(_.Value, jsonqlAuthorization, jsonqlIncluder, jsonqlLinqer));
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

                var template_resource = segments[0];
                var template_enumeration = segments[1];

                var includes = new IncludeParser(template_enumeration, templates).Parse();

                var value = await getValue2(template_resource, includes, variables, @object);
                var queryable = value is Resource
                    ? (value as Resource)?.Queryable
                    : (value as IEnumerable)?.AsQueryable();
                if (queryable == null)
                    return null;

                var isArrayReturn = template_enumeration.EndsWith("[]");
                if (isArrayReturn)
                    template_enumeration = template_enumeration.Substring(0, template_enumeration.Length - 2);

                if (jsonqlLinqer != null)
                {
                    var selector = new SelectParser(templates).Parse(template_enumeration);
                    if (!string.IsNullOrWhiteSpace(selector))
                        queryable = jsonqlLinqer.Select(queryable, selector);
                }

                if (isArrayReturn)
                {
                    var list = new List<object>();
                    var source = jsonqlLinqer == null ? QueryableHelper.ToList(queryable) : jsonqlLinqer.ToList(queryable);
                    foreach (var item in source)
                    {
                        list.Add(await getValue1(template_enumeration, variables, false, item));
                    }
                    return list;
                }
                else
                {
                    var first = jsonqlLinqer == null
                        ? QueryableHelper.FirstOrDefault(queryable)
                        : jsonqlLinqer.FirstOrDefault(queryable);
                    if (first == null)
                        return null;

                    return await getValue1(template_enumeration, variables, false, first);
                }
            }
            //取值
            else
            {
                var value = await getValue2(template, null, variables, @object);
                if (value is Resource)
                {
                    if (isVariable)
                        return value;
                    else
                    {
                        var queryable = (value as Resource).Queryable;
                        return jsonqlLinqer == null
                            ? QueryableHelper.ToList(queryable)
                            : jsonqlLinqer.ToList(queryable);
                    }
                }
                return value;
            }
        }

        async Task<object> getValue2(string template, string[] includes, IDictionary<string, object> variables, object @object = null)
        {
            if ("$".Equals(template))//对象自身
                return @object;
            else if (template.StartsWith("$."))//对象的属性或方法
                return @object.GetValue(template.Substring(2));
            else if (template.StartsWith("`"))//表达式
                return evaluateExpression(expressions[template], ref variables);
            else if (template.StartsWith("#"))//模板
                return await createObject(templates[template], variables, @object);
            else if (template.StartsWith("@"))//资源
                return createResource(template, includes, variables);
            else if (template.StartsWith("$"))//变量
                return createResource(template, includes, variables);
            else
                return JsonConvert.DeserializeObject(template);
        }

        object createResource(string template, string[] includes, IDictionary<string, object> variables)
        {
            object @return = null;

            string[] segments = null;
            using (var accessParser = new AccessParser(template))
            {
                segments = accessParser.Parse();
            }

            var segment0 = segments[0];
            if (segment0.StartsWith("@"))
                @return = resources[segment0].Include(includes);
            else if (segment0.StartsWith("$"))
                @return = variables[segment0];

            for (var i = 1; i < segments.Length; i++)
            {
                var segment = segments[i];
                if (@return is Resource)
                {
                    var method = new MethodParser(segment).Parse();
                    var parameters = method.Variables.ToDictionary(_ => _, _ => variables[_]);
                    var resource = (@return as Resource).SetParameters(parameters);
                    switch (method.Name.ToLower())
                    {
                        case "all":
                            @return = resource.All(method.Parameter);
                            break;
                        case "any":
                            @return = string.IsNullOrEmpty(method.Parameter)
                                ? resource.Any()
                                : resource.Any(method.Parameter);
                            break;
                        case "average":
                            @return = string.IsNullOrEmpty(method.Parameter)
                                ? resource.Average()
                                : resource.Average(method.Parameter);
                            break;
                        case "count":
                            @return = string.IsNullOrEmpty(method.Parameter)
                                ? resource.Count()
                                : resource.Count(method.Parameter);
                            break;
                        case "distinct":
                            @return = resource.Distinct();
                            break;
                        case "groupby":
                            @return = resource.GroupBy(method.Parameter);
                            break;
                        case "max":
                            @return = string.IsNullOrEmpty(method.Parameter)
                                ? resource.Max()
                                : resource.Max(method.Parameter);
                            break;
                        case "min":
                            @return = string.IsNullOrEmpty(method.Parameter)
                                ? resource.Min()
                                : resource.Min(method.Parameter);
                            break;
                        case "orderby":
                            @return = resource.OrderBy(method.Parameter);
                            break;
                        case "orderbydescending":
                            @return = resource.OrderByDescending(method.Parameter);
                            break;
                        case "select":
                            @return = resource.Select(method.Parameter);
                            break;
                        case "skip":
                            @return = resource.Skip(int.Parse(method.Parameter));
                            break;
                        case "sum":
                            @return = string.IsNullOrEmpty(method.Parameter)
                                ? resource.Sum()
                                : resource.Sum(method.Parameter);
                            break;
                        case "take":
                            @return = resource.Take(int.Parse(method.Parameter));
                            break;
                        case "thenby":
                            @return = (resource as Resource_Ordered).ThenBy(method.Parameter);
                            break;
                        case "thenbydescending":
                            @return = (resource as Resource_Ordered).ThenByDescending(method.Parameter);
                            break;
                        case "where":
                            @return = resource.Where(method.Parameter);
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

        object evaluateExpression(string expression, ref IDictionary<string, object> variables)
        {
            return jsonqlEvaluator.Evaluate(expression.TrimStart('{').TrimEnd('}'), ref variables);
        }
    }
}
