using System.Collections.Generic;
using Liyanjie.Evaluation;
using Liyanjie.Jsonql.Core;

namespace Liyanjie.Jsonql.DynamicEvaluation
{
    /// <summary>
    /// 
    /// </summary>
    public class DynamicJsonqlEvaluator : IJsonqlEvaluator
    {
        /// <inheritdoc />
        public object Evaluate(string expression, ref IDictionary<string, object> variables)
        {
            return Evaluator.Evaluate(expression, ref variables);
        }
    }
}