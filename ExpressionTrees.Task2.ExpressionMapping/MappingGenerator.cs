using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionTrees.Task2.ExpressionMapping
{
    public class MappingGenerator
    {
        public Mapper<TSource, TDestination> Generate<TSource, TDestination>(Dictionary<string, string> mapping)
        {
            var sourceParam = Expression.Parameter(typeof(TSource), "source");
            var bindings = new List<MemberBinding>();

            foreach (var map in mapping)
            {
                var sourceProperty = typeof(TSource).GetProperty(map.Key);
                var destinationProperty = typeof(TDestination).GetProperty(map.Value);
                
                var sourcePropertyAccess = Expression.Property(sourceParam, sourceProperty);
                var conversion = Expression.Convert(sourcePropertyAccess, destinationProperty.PropertyType);
                var binding = Expression.Bind(destinationProperty, conversion);

                bindings.Add(binding);
            }

            var body = Expression.MemberInit(Expression.New(typeof(TDestination)), bindings);
            var mapFunction = Expression.Lambda<Func<TSource, TDestination>>(body, sourceParam);

            return new Mapper<TSource, TDestination>(mapFunction.Compile());
        }
    }
}