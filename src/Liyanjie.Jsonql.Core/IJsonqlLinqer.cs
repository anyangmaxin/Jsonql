using System.Collections.Generic;
using System.Linq;

namespace Liyanjie.Jsonql.Core
{
    /// <summary>
    /// 
    /// </summary>
    public interface IJsonqlLinqer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="predicate"></param>
        /// <param name="variables"></param>
        /// <returns></returns>
        bool All(IQueryable queryable, string predicate, IDictionary<string, dynamic> variables = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        bool Any(IQueryable queryable);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="predicate"></param>
        /// <param name="variables"></param>
        /// <returns></returns>
        bool Any(IQueryable queryable, string predicate, IDictionary<string, dynamic> variables = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        object Average(IQueryable queryable);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="selector"></param>
        /// <param name="variables"></param>
        /// <returns></returns>
        object Average(IQueryable queryable, string selector, IDictionary<string, dynamic> variables = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        int Count(IQueryable queryable);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="predicate"></param>
        /// <param name="variables"></param>
        /// <returns></returns>
        int Count(IQueryable queryable, string predicate, IDictionary<string, dynamic> variables = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        IQueryable Distinct(IQueryable queryable);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="queryable"></param>
        ///// <param name="index"></param>
        ///// <returns></returns>
        //object ElementAt(IQueryable queryable, int index);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="queryable"></param>
        ///// <param name="index"></param>
        ///// <returns></returns>
        //object ElementAtOrDefault(IQueryable queryable, int index);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="queryable"></param>
        ///// <returns></returns>
        //object First(IQueryable queryable);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="queryable"></param>
        ///// <param name="predicate"></param>
        ///// <param name="variables"></param>
        ///// <returns></returns>
        //object First(IQueryable queryable, string predicate, IDictionary<string, dynamic> variables = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        object FirstOrDefault(IQueryable queryable);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="predicate"></param>
        /// <param name="variables"></param>
        /// <returns></returns>
        object FirstOrDefault(IQueryable queryable, string predicate, IDictionary<string, dynamic> variables = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="keySelector"></param>
        /// <param name="variables"></param>
        /// <returns></returns>
        IQueryable GroupBy(IQueryable queryable, string keySelector, IDictionary<string, dynamic> variables = null);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="source"></param>
        ///// <returns></returns>
        //object Last(IQueryable source);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="source"></param>
        ///// <param name="predicate"></param>
        ///// <param name="variables"></param>
        ///// <returns></returns>
        //object Last(IQueryable source, string predicate, IDictionary<string, dynamic> variables = null);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="source"></param>
        ///// <returns></returns>
        //object LastOrDefault(IQueryable source);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="source"></param>
        ///// <param name="predicate"></param>
        ///// <param name="variables"></param>
        ///// <returns></returns>
        //object LastOrDefault(IQueryable source, string predicate, IDictionary<string, dynamic> variables = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        object Max(IQueryable queryable);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="selector"></param>
        /// <param name="variables"></param>
        /// <returns></returns>
        object Max(IQueryable queryable, string selector, IDictionary<string, dynamic> variables = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        object Min(IQueryable queryable);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="selector"></param>
        /// <param name="variables"></param>
        /// <returns></returns>
        object Min(IQueryable queryable, string selector, IDictionary<string, dynamic> variables = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="keySelector"></param>
        /// <param name="variables"></param>
        /// <returns></returns>
        IOrderedQueryable OrderBy(IQueryable queryable, string keySelector, IDictionary<string, dynamic> variables = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="keySelector"></param>
        /// <param name="variables"></param>
        /// <returns></returns>
        IOrderedQueryable OrderByDescending(IQueryable queryable, string keySelector, IDictionary<string, dynamic> variables = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IQueryable Reverse(IQueryable source);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="selector"></param>
        /// <param name="variables"></param>
        /// <returns></returns>
        IQueryable Select(IQueryable queryable, string selector, IDictionary<string, dynamic> variables = null);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="source"></param>
        ///// <returns></returns>
        //object Single(IQueryable source);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="source"></param>
        ///// <param name="predicate"></param>
        ///// <param name="variables"></param>
        ///// <returns></returns>
        //object Single(IQueryable source, string predicate, IDictionary<string, dynamic> variables = null);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="source"></param>
        ///// <returns></returns>
        //object SingleOrDefault(IQueryable source);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="source"></param>
        ///// <param name="predicate"></param>
        ///// <param name="variables"></param>
        ///// <returns></returns>
        //object SingleOrDefault(IQueryable source, string predicate, IDictionary<string, dynamic> variables = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        IQueryable Skip(IQueryable queryable, int count);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        object Sum(IQueryable queryable);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="valueSelector"></param>
        /// <param name="variables"></param>
        /// <returns></returns>
        object Sum(IQueryable queryable, string valueSelector, IDictionary<string, dynamic> variables = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        IQueryable Take(IQueryable queryable, int count);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="keySelector"></param>
        /// <param name="variables"></param>
        /// <returns></returns>
        IOrderedQueryable ThenBy(IOrderedQueryable queryable, string keySelector, IDictionary<string, dynamic> variables = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="keySelector"></param>
        /// <param name="variables"></param>
        /// <returns></returns>
        IOrderedQueryable ThenByDescending(IOrderedQueryable queryable, string keySelector, IDictionary<string, dynamic> variables = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        List<object> ToList(IQueryable queryable);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="predicate"></param>
        /// <param name="variables"></param>
        /// <returns></returns>
        IQueryable Where(IQueryable queryable, string predicate, IDictionary<string, dynamic> variables = null);
    }
}
