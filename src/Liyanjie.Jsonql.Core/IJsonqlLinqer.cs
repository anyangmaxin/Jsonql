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
        /// <param name="parameters"></param>
        /// <returns></returns>
        bool All(IQueryable queryable, string predicate, object[] parameters);

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
        /// <param name="parameters"></param>
        /// <returns></returns>
        bool Any(IQueryable queryable, string predicate, object[] parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="valueSelector"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        object Average<TResult>(IQueryable queryable, string valueSelector, object[] parameters) where TResult : struct;

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
        /// <param name="parameters"></param>
        /// <returns></returns>
        int Count(IQueryable queryable, string predicate, object[] parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        IQueryable Distinct(IQueryable queryable);

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
        /// <param name="keySelector"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IQueryable GroupBy(IQueryable queryable, string keySelector, object[] parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="valueSelector"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        object Max<TResult>(IQueryable queryable, string valueSelector, object[] parameters) where TResult : struct;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="valueSelector"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        object Min<TResult>(IQueryable queryable, string valueSelector, object[] parameters) where TResult : struct;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="keySelector"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IOrderedQueryable OrderBy(IQueryable queryable, string keySelector, object[] parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="keySelector"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IOrderedQueryable OrderByDescending(IQueryable queryable, string keySelector, object[] parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="selector"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IQueryable Select(IQueryable queryable, string selector, object[] parameters);

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
        /// <param name="valueSelector"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        object Sum<TResult>(IQueryable queryable, string valueSelector, object[] parameters) where TResult : struct;

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
        /// <param name="parameters"></param>
        /// <returns></returns>
        IOrderedQueryable ThenBy(IOrderedQueryable queryable, string keySelector, object[] parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="keySelector"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IOrderedQueryable ThenByDescending(IOrderedQueryable queryable, string keySelector, object[] parameters);

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
        /// <param name="parameters"></param>
        /// <returns></returns>
        IQueryable Where(IQueryable queryable, string predicate, object[] parameters);
    }
}
