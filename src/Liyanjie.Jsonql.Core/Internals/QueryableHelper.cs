using System.Collections.Generic;
using System.Linq;

namespace Liyanjie.Jsonql.Core.Internals
{
    internal class QueryableHelper
    {
        public static object FirstOrDefault(IQueryable querable)
        {
            var enumerator = querable.GetEnumerator();
            if (enumerator.MoveNext())
                return enumerator.Current;
            else
                return null;
        }

        public static List<object> ToList(IQueryable querable)
        {
            var list = new List<object>();
            var enumerator = querable.GetEnumerator();
            while (enumerator.MoveNext())
            {
                list.Add(enumerator.Current);
            }
            return list;
        }
    }
}
