using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ExpressionTrees.Task1.ExpressionsTransformer
{
    public class IncDecExpressionVisitor : ExpressionVisitor
    {
        private readonly Dictionary<string, ConstantExpression> _parameters;

        public IncDecExpressionVisitor(Dictionary<string, object> parameters)
        {
            _parameters = parameters.ToDictionary(
                pair => pair.Key,
                pair => Expression.Constant(pair.Value));
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (_parameters.TryGetValue(node.Name, out var replacement))
            {
                return replacement;
            }

            return base.VisitParameter(node);
        }

        public Expression Transform(Expression<Func<int, int>> expression)
        {
            return Visit(expression.Body);
        }
    }
}