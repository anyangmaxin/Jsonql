using System.Linq;

namespace Liyanjie.Jsonql.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class Resource
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="jsonqlIncluder"></param>
        /// <param name="jsonqlLinqer"></param>
        public Resource(IQueryable queryable, IJsonqlIncluder jsonqlIncluder, IJsonqlLinqer jsonqlLinqer)
        {
            Queryable = queryable;
            JsonqlIncluder = jsonqlIncluder;
            JsonqlLinqer = jsonqlLinqer;
        }

        /// <summary>
        /// 
        /// </summary>
        internal IQueryable Queryable { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        protected readonly IJsonqlIncluder JsonqlIncluder;

        /// <summary>
        /// 
        /// </summary>
        protected readonly IJsonqlLinqer JsonqlLinqer;

        /// <summary>
        /// 
        /// </summary>
        protected object[] parameters { get; private set; }

        internal Resource SetParameters(params object[] parameters)
        {
            this.parameters = parameters;
            return this;
        }

        internal Resource Include(params string[] paths)
        {
            if (JsonqlIncluder != null)
                Queryable = JsonqlIncluder.Include(Queryable, paths);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public bool All(string predicate)
        {
            return JsonqlLinqer.All(Queryable, predicate, parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public bool Any(string predicate = null)
        {
            if (predicate == null)
                return JsonqlLinqer.Any(Queryable);
            else
                return JsonqlLinqer.Any(Queryable, predicate, parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public object Average(string selector)
        {
            return JsonqlLinqer.Average<double>(Queryable, selector, parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int Count(string predicate = null)
        {
            if (predicate == null)
                return JsonqlLinqer.Count(Queryable);
            else
                return JsonqlLinqer.Count(Queryable, predicate, parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Resource Distinct()
        {
            return new Resource(JsonqlLinqer.Distinct(Queryable), JsonqlIncluder, JsonqlLinqer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public Resource GroupBy(string selector)
        {
            return new Resource(JsonqlLinqer.GroupBy(Queryable, selector, parameters), JsonqlIncluder, JsonqlLinqer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public object Max(string selector)
        {
            return JsonqlLinqer.Max<double>(Queryable, selector, parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public object Min(string selector)
        {
            return JsonqlLinqer.Min<double>(Queryable, selector, parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public Resource_Ordered OrderBy(string selector)
        {
            return new Resource_Ordered(JsonqlLinqer.OrderBy(Queryable, selector, parameters), JsonqlIncluder, JsonqlLinqer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public Resource_Ordered OrderByDescending(string selector)
        {
            return new Resource_Ordered(JsonqlLinqer.OrderByDescending(Queryable, selector, parameters), JsonqlIncluder, JsonqlLinqer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public Resource Select(string selector)
        {
            return new Resource(JsonqlLinqer.Select(Queryable, selector, parameters), JsonqlIncluder, JsonqlLinqer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public Resource Skip(int count)
        {
            return new Resource(JsonqlLinqer.Skip(Queryable, count), JsonqlIncluder, JsonqlLinqer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public object Sum(string selector)
        {
            return JsonqlLinqer.Sum<double>(Queryable, selector, parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public Resource Take(int count)
        {
            return new Resource(JsonqlLinqer.Take(Queryable, count), JsonqlIncluder, JsonqlLinqer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Resource Where(string predicate)
        {
            return new Resource(JsonqlLinqer.Where(Queryable, predicate, parameters), JsonqlIncluder, JsonqlLinqer);
        }

        /// <summary>
        /// 
        /// </summary>
        ~Resource()
        {
            Queryable = null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Resource_Ordered : Resource
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderedQueryable"></param>
        /// <param name="jsonqlIncluder"></param>
        /// <param name="jsonqlLinqer"></param>
        public Resource_Ordered(IOrderedQueryable orderedQueryable, IJsonqlIncluder jsonqlIncluder, IJsonqlLinqer jsonqlLinqer) : base(orderedQueryable, jsonqlIncluder, jsonqlLinqer)
        {
            OrderedQueryable = orderedQueryable;
        }

        internal IOrderedQueryable OrderedQueryable { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public Resource_Ordered ThenBy(string selector)
        {
            return new Resource_Ordered(JsonqlLinqer.ThenBy(OrderedQueryable, selector, parameters), JsonqlIncluder, JsonqlLinqer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public Resource_Ordered ThenByDescending(string selector)
        {
            return new Resource_Ordered(JsonqlLinqer.ThenByDescending(OrderedQueryable, selector, parameters), JsonqlIncluder, JsonqlLinqer);
        }

        /// <summary>
        /// 
        /// </summary>
        ~Resource_Ordered()
        {
            OrderedQueryable = null;
        }
    }
}
