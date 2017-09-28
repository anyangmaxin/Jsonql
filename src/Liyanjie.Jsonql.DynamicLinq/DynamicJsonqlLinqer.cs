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
        public bool All(IQueryable queryable, string predicate, IDictionary<string, dynamic> variables = null)
        {
            return queryable.All(predicate, variables);
        }

        /// <inheritdoc />
        public bool Any(IQueryable queryable)
        {
            return queryable.Any();
        }

        /// <inheritdoc />
        public bool Any(IQueryable queryable, string predicate, IDictionary<string, dynamic> variables = null)
        {
            return queryable.Any(predicate, variables);
        }

        /// <inheritdoc />
        public object Average(IQueryable queryable)
        {
            return queryable.Average();
        }

        /// <inheritdoc />
        public object Average(IQueryable queryable, string valueSelector, IDictionary<string, dynamic> variables = null)
        {
            return queryable.Average(valueSelector, variables);
        }

        /// <inheritdoc />
        public int Count(IQueryable queryable)
        {
            return queryable.Count();
        }

        /// <inheritdoc />
        public int Count(IQueryable queryable, string predicate, IDictionary<string, dynamic> variables = null)
        {
            return queryable.Count(predicate, variables);
        }

        /// <inheritdoc />
        public IQueryable Distinct(IQueryable queryable)
        {
            return queryable.Distinct();
        }

        /// <inheritdoc />
        public object ElementAt(IQueryable queryable, int index)
        {
            return queryable.ElementAt(index);
        }

        /// <inheritdoc />
        public object ElementAtOrDefault(IQueryable queryable, int index)
        {
            return queryable.ElementAtOrDefault(index);
        }

        /// <inheritdoc />
        public object First(IQueryable queryable)
        {
            return queryable.First();
        }

        /// <inheritdoc />
        public object First(IQueryable queryable, string predicate, IDictionary<string, dynamic> variables = null)
        {
            return queryable.First(predicate, variables);
        }

        /// <inheritdoc />
        public object FirstOrDefault(IQueryable queryable)
        {
            return queryable.FirstOrDefault();
        }

        /// <inheritdoc />
        public object FirstOrDefault(IQueryable queryable, string predicate, IDictionary<string, dynamic> variables = null)
        {
            return queryable.FirstOrDefault(predicate, variables);
        }

        /// <inheritdoc />
        public IQueryable GroupBy(IQueryable queryable, string keySelector, IDictionary<string, dynamic> variables = null)
        {
            return queryable.GroupBy(keySelector, variables);
        }

        /// <inheritdoc />
        public object Last(IQueryable queryable)
        {
            return queryable.Last();
        }

        /// <inheritdoc />
        public object Last(IQueryable queryable, string predicate, IDictionary<string, dynamic> variables = null)
        {
            return queryable.Last(predicate, variables);
        }

        /// <inheritdoc />
        public object LastOrDefault(IQueryable queryable)
        {
            return queryable.LastOrDefault();
        }

        /// <inheritdoc />
        public object LastOrDefault(IQueryable queryable, string predicate, IDictionary<string, dynamic> variables = null)
        {
            return queryable.LastOrDefault(predicate, variables);
        }

        /// <inheritdoc />
        public object Max(IQueryable queryable)
        {
            return queryable.Max();
        }

        /// <inheritdoc />
        public object Max(IQueryable queryable, string selector, IDictionary<string, dynamic> variables = null)
        {
            return queryable.Max(selector, variables);
        }

        /// <inheritdoc />
        public object Min(IQueryable queryable)
        {
            return queryable.Min();
        }

        /// <inheritdoc />
        public object Min(IQueryable queryable, string selector, IDictionary<string, dynamic> variables = null)
        {
            return queryable.Min(selector, variables);
        }

        /// <inheritdoc />
        public IOrderedQueryable OrderBy(IQueryable queryable, string keySelector, IDictionary<string, dynamic> variables = null)
        {
            return queryable.OrderBy(keySelector, variables);
        }

        /// <inheritdoc />
        public IOrderedQueryable OrderByDescending(IQueryable queryable, string keySelector, IDictionary<string, dynamic> variables = null)
        {
            return queryable.OrderByDescending(keySelector, variables);
        }

        /// <inheritdoc />
        public IQueryable Reverse(IQueryable queryable)
        {
            return queryable.Reverse();
        }

        /// <inheritdoc />
        public IQueryable Select(IQueryable queryable, string selector, IDictionary<string, dynamic> variables = null)
        {
            return queryable.Select(selector, variables);
        }

        /// <inheritdoc />
        public object Single(IQueryable queryable)
        {
            return queryable.Single();
        }

        /// <inheritdoc />
        public object Single(IQueryable queryable, string predicate, IDictionary<string, dynamic> variables = null)
        {
            return queryable.Single(predicate, variables);
        }

        /// <inheritdoc />
        public object SingleOrDefault(IQueryable queryable)
        {
            return queryable.SingleOrDefault();
        }

        /// <inheritdoc />
        public object SingleOrDefault(IQueryable queryable, string predicate, IDictionary<string, dynamic> variables = null)
        {
            return queryable.SingleOrDefault(predicate, variables);
        }

        /// <inheritdoc />
        public IQueryable Skip(IQueryable queryable, int count)
        {
            return queryable.Skip(count);
        }

        /// <inheritdoc />
        public object Sum(IQueryable queryable)
        {
            return queryable.Sum();
        }

        /// <inheritdoc />
        public object Sum(IQueryable queryable, string selector, IDictionary<string, dynamic> variables = null)
        {
            return queryable.Sum(selector, variables);
        }

        /// <inheritdoc />
        public IQueryable Take(IQueryable queryable, int count)
        {
            return queryable.Take(count);
        }

        /// <inheritdoc />
        public IOrderedQueryable ThenBy(IOrderedQueryable queryable, string keySelector, IDictionary<string, dynamic> variables = null)
        {
            return queryable.ThenBy(keySelector, variables);
        }

        /// <inheritdoc />
        public IOrderedQueryable ThenByDescending(IOrderedQueryable queryable, string keySelector, IDictionary<string, dynamic> variables = null)
        {
            return queryable.ThenByDescending(keySelector, variables);
        }

        /// <inheritdoc />
        public List<object> ToList(IQueryable queryable)
        {
            return queryable.ToList();
        }

        /// <inheritdoc />
        public IQueryable Where(IQueryable queryable, string predicate, IDictionary<string, dynamic> variables = null)
        {
            return queryable.Where(predicate, variables);
        }
    }
}