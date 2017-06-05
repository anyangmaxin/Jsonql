using System.Collections.Generic;

namespace Liyanjie.Jsonql.Core
{
    /// <summary>
    /// 
    /// </summary>
    public interface IJsonqlEvaluator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="variables"></param>
        /// <returns></returns>
        object Evaluate(string expression, IDictionary<string, object> variables);
    }
}
