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
        /// <param name="dynamicLinq"></param>
        public Resource(IQueryable queryable, IDynamicLinq dynamicLinq)
        {
            Queryable = queryable;
            DynamicLinq = dynamicLinq;
        }

        /// <summary>
        /// 
        /// </summary>
        protected readonly IDynamicLinq DynamicLinq;

        /// <summary>
        /// 
        /// </summary>
        protected object[] parameters { get; private set; }

        internal IQueryable Queryable { get; private set; }

        internal Resource SetParameters(params object[] parameters)
        {
            this.parameters = parameters;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public bool All(string predicate)
        {
            return DynamicLinq.All(Queryable, predicate, parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public bool Any(string predicate = null)
        {
            if (predicate == null)
                return DynamicLinq.Any(Queryable);
            else
                return DynamicLinq.Any(Queryable, predicate, parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public object Average(string selector)
        {
            return DynamicLinq.Average<double>(Queryable, selector, parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int Count(string predicate = null)
        {
            if (predicate == null)
                return DynamicLinq.Count(Queryable);
            else
                return DynamicLinq.Count(Queryable, predicate, parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Resource Distinct()
        {
            return new Resource(DynamicLinq.Distinct(Queryable), DynamicLinq);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public Resource GroupBy(string selector)
        {
            return new Resource(DynamicLinq.GroupBy(Queryable, selector, parameters), DynamicLinq);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public object Max(string selector)
        {
            return DynamicLinq.Max<double>(Queryable, selector, parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public object Min(string selector)
        {
            return DynamicLinq.Min<double>(Queryable, selector, parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public Resource_Ordered OrderBy(string selector)
        {
            return new Resource_Ordered(DynamicLinq.OrderBy(Queryable, selector, parameters), DynamicLinq);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public Resource_Ordered OrderByDescending(string selector)
        {
            return new Resource_Ordered(DynamicLinq.OrderByDescending(Queryable, selector, parameters), DynamicLinq);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public Resource Select(string selector)
        {
            return new Resource(DynamicLinq.Select(Queryable, selector, parameters), DynamicLinq);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public Resource Skip(int count)
        {
            return new Resource(DynamicLinq.Skip(Queryable, count), DynamicLinq);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public object Sum(string selector)
        {
            return DynamicLinq.Sum<double>(Queryable, selector, parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public Resource Take(int count)
        {
            return new Resource(DynamicLinq.Take(Queryable, count), DynamicLinq);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Resource Where(string predicate)
        {
            return new Resource(DynamicLinq.Where(Queryable, predicate, parameters), DynamicLinq);
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
        /// <param name="dynamicLinq"></param>
        public Resource_Ordered(IOrderedQueryable orderedQueryable, IDynamicLinq dynamicLinq) : base(orderedQueryable, dynamicLinq)
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
            return new Resource_Ordered(DynamicLinq.ThenBy(OrderedQueryable, selector, parameters), DynamicLinq);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public Resource_Ordered ThenByDescending(string selector)
        {
            return new Resource_Ordered(DynamicLinq.ThenByDescending(OrderedQueryable, selector, parameters), DynamicLinq);
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
