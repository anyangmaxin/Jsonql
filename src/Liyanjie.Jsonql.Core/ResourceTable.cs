using System;
using System.Collections.Generic;
using System.Linq;

namespace Liyanjie.Jsonql.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class ResourceTable
    {
        readonly IDictionary<string, Type> types = new Dictionary<string, Type>();
        readonly IDictionary<string, IQueryable> queryables = new Dictionary<string, IQueryable>();
        readonly IDictionary<string, Action<IQueryable, IJsonqlAuthorization>> filters = new Dictionary<string, Action<IQueryable, IJsonqlAuthorization>>();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="queryable"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public ResourceTable AddResource<T>(string name, IQueryable queryable, Action<IQueryable, IJsonqlAuthorization> filter = null)
        {
            types.Add(name, typeof(T));
            queryables.Add(name, queryable);
            filters.Add(name, filter);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="queryable"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public ResourceTable AddResource<T>(string name, IQueryable<T> queryable, Action<IQueryable, IJsonqlAuthorization> filter = null)
        {
            types.Add(name, typeof(T));
            queryables.Add(name, queryable);
            filters.Add(name, filter);
            return this;
        }

        internal Resource GetResource(string template, IJsonqlAuthorization jsonqlAuthorization, IJsonqlIncluder jsonqlIncluder = null, IJsonqlLinqer jsonqlLinqer = null)
        {
            var name = template.Substring(0, template.IndexOf("[]"));

            var queryable = queryables[name];
            if (queryable == null)
                return null;

            if (filters.ContainsKey(name))
                filters[name]?.Invoke(queryable, jsonqlAuthorization);

            return new Resource(queryable, jsonqlIncluder, jsonqlLinqer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, Type> ResourceTypes => types;
    }
}
