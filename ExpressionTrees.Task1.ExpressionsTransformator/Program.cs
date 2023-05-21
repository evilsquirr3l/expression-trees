﻿/*
 * Create a class based on ExpressionVisitor, which makes expression tree transformation:
 * 1. converts expressions like <variable> + 1 to increment operations, <variable> - 1 - into decrement operations.
 * 2. changes parameter values in a lambda expression to constants, taking the following as transformation parameters:
 *    - source expression;
 *    - dictionary: <parameter name: value for replacement>
 * The results could be printed in console or checked via Debugger using any Visualizer.
 */
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionTrees.Task1.ExpressionsTransformer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Expression Visitor for increment/decrement.");
            Console.WriteLine();

            Expression<Func<int, int>> expression = x => x + 1;
            Console.WriteLine("Original Expression: " + expression);

            var expressionVisitor = new IncDecExpressionVisitor(new Dictionary<string, object> { { "x", 5 } });
    
            var transformed = expressionVisitor.Transform(expression);
            Console.WriteLine("Transformed Expression: " + transformed);
    
            var compiled = Expression.Lambda<Func<int>>(transformed).Compile();
            Console.WriteLine("Result: " + compiled());
        }
    }
}
