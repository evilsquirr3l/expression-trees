using System;
using System.Collections.Generic;
using ExpressionTrees.Task2.ExpressionMapping.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExpressionTrees.Task2.ExpressionMapping.Tests
{
    [TestClass]
    public class ExpressionMappingTests
    {
        [TestMethod]
        public void MappingBetweenDifferentProperties()
        {
            var mappingGenerator = new MappingGenerator();
            var mapper = mappingGenerator.Generate<Bar, Foo>(
                new Dictionary<string, string>
                {
                    { "Income", "Salary" }
                }
            );

            var bar = new Bar { Id = Guid.NewGuid().ToString(), Income = 123.45M };
            var foo = mapper.Map(bar);

            Assert.AreEqual(bar.Income, foo.Salary);
        }
        
        [TestMethod]
        public void MappingBetweenDifferentTypes()
        {
            var mappingGenerator = new MappingGenerator();
            var mapper = mappingGenerator.Generate<Bar, Foo>(
                new Dictionary<string, string>
                {
                    { "Id", "Guid" }
                }
            );

            var bar = new Bar { Id = Guid.NewGuid().ToString(), Income = 123.45M };
            var foo = mapper.Map(bar);

            Assert.AreEqual(bar.Id, foo.Guid.ToString());
        }
    }
}
