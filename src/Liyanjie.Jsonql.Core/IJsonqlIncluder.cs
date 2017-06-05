using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liyanjie.Jsonql.Core
{
    /// <summary>
    /// 
    /// </summary>
    public interface IJsonqlIncluder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="navigationPropertyPath"></param>
        /// <returns></returns>
        IQueryable Include(IQueryable queryable, params string[] navigationPropertyPath);
    }
}
