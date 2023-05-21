using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionTrees.Task2.ExpressionMapping
{
    public class MappingGenerator
    {
        public Mapper<TSource, TDestination> Generate<TSource, TDestination>(Dictionary<string, string> mappings)
        {
            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination);

            var sourceParam = Expression.Parameter(sourceType, "source");
            var bindings = new List<MemberBinding>();

            foreach (var map in mappings)
            {
                var sourceProperty = sourceType.GetProperty(map.Key);
                var destinationProperty = destinationType.GetProperty(map.Value);

                var sourcePropertyAccess = Expression.Property(sourceParam, sourceProperty);
                Expression conversionExpression;

                if (destinationProperty.PropertyType != sourceProperty.PropertyType)
                {
                    if (sourceProperty.PropertyType == typeof(string) &&
                        destinationProperty.PropertyType == typeof(Guid))
                    {
                        conversionExpression = Expression.Call(typeof(Guid), "Parse", Type.EmptyTypes,
                            sourcePropertyAccess);
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    conversionExpression = sourcePropertyAccess;
                }

                var binding = Expression.Bind(destinationProperty, conversionExpression);
                bindings.Add(binding);
            }

            var body = Expression.MemberInit(Expression.New(destinationType), bindings);
            var mapFunction = Expression.Lambda<Func<TSource, TDestination>>(body, sourceParam);

            return new Mapper<TSource, TDestination>(mapFunction.Compile());
        }
    }
}