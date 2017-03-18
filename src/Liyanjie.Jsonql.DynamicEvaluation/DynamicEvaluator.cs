﻿using Liyanjie.Evaluation;
using Liyanjie.Jsonql.Core;
using System.Collections.Generic;

namespace Liyanjie.Jsonql.DynamicEvaluation
{
    /// <summary>
    /// 
    /// </summary>
    public class DynamicEvaluator : IDynamicEvaluator
    {
        /// <inheritdoc />
        public object Evaluate(string expression, IDictionary<string, object> variables)
        {
            return Evaluator.Evaluate(expression, variables);
        }
    }
}