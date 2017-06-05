using System.Collections.Generic;
using System.Linq;
using Liyanjie.Jsonql.Core;

namespace Liyanjie.Jsonql.DynamicLinq
{
    /// <summary>
    /// 
    /// </summary>
    public class DynamicJsonqlLinqer : IJsonqlLinqer
    {
        /// <inheritdoc />
        public bool All(IQueryable queryable, string predicate, object[] parameters)
        {
            return queryable.All(predicate, parameters);
        }

        /// <inheritdoc />
        public bool Any(IQueryable queryable)
        {
            return queryable.Any();
        }

        /// <inheritdoc />
        public bool Any(IQueryable queryable, string predicate, object[] parameters)
        {
            return queryable.Any(predicate, parameters);
        }

        /// <inheritdoc />
        public object Average<TResult>(IQueryable queryable, string valueSelector, object[] parameters) where TResult : struct
        {
            return queryable.Average(typeof(TResult), valueSelector, parameters);
        }

        /// <inheritdoc />
        public int Count(IQueryable queryable)
        {
            return queryable.Count();
        }

        /// <inheritdoc />
        public int Count(IQueryable queryable, string predicate, object[] parameters)
        {
            return queryable.Count(predicate, parameters);
        }

        /// <inheritdoc />
        public IQueryable Distinct(IQueryable queryable)
        {
            return queryable.Distinct();
        }

        /// <inheritdoc />
        public object FirstOrDefault(IQueryable queryable)
        {
            return queryable.FirstOrDefault();
        }

        /// <inheritdoc />
        public IQueryable GroupBy(IQueryable queryable, string keySelector, object[] parameters)
        {
            return queryable.GroupBy(keySelector, parameters);
        }

        /// <inheritdoc />
        public object Max<TResult>(IQueryable queryable, string valueSelector, object[] parameters) where TResult : struct
        {
            return queryable.Max(typeof(TResult), valueSelector, parameters);
        }

        /// <inheritdoc />
        public object Min<TResult>(IQueryable queryable, string valueSelector, object[] parameters) where TResult : struct
        {
            return queryable.Min(typeof(TResult), valueSelector, parameters);
        }

        /// <inheritdoc />
        public IOrderedQueryable OrderBy(IQueryable queryable, string keySelector, object[] parameters)
        {
            return queryable.OrderBy(keySelector, parameters);
        }

        /// <inheritdoc />
        public IOrderedQueryable OrderByDescending(IQueryable queryable, string keySelector, object[] parameters)
        {
            return queryable.OrderByDescending(keySelector, parameters);
        }

        /// <inheritdoc />
        public IQueryable Select(IQueryable queryable, string selector, object[] parameters)
        {
            return queryable.Select(selector, parameters);
        }

        /// <inheritdoc />
        public IQueryable Skip(IQueryable queryable, int count)
        {
            return queryable.Skip(count);
        }

        /// <inheritdoc />
        public object Sum<TResult>(IQueryable queryable, string valueSelector, object[] parameters) where TResult : struct
        {
            return queryable.Sum(typeof(TResult), valueSelector, parameters);
        }

        /// <inheritdoc />
        public IQueryable Take(IQueryable queryable, int count)
        {
            return queryable.Take(count);
        }

        /// <inheritdoc />
        public IOrderedQueryable ThenBy(IOrderedQueryable queryable, string keySelector, object[] parameters)
        {
            return queryable.ThenBy(keySelector, parameters);
        }

        /// <inheritdoc />
        public IOrderedQueryable ThenByDescending(IOrderedQueryable queryable, string keySelector, object[] parameters)
        {
            return queryable.ThenByDescending(keySelector, parameters);
        }

        /// <inheritdoc />
        public List<object> ToList(IQueryable queryable)
        {
            return queryable.ToList();
        }

        /// <inheritdoc />
        public IQueryable Where(IQueryable queryable, string predicate, object[] parameters)
        {
            return queryable.Where(predicate, parameters);
        }
    }
}