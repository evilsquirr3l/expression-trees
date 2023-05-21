using System;
using System.Collections.Generic;
using ExpressionTrees.Task2.ExpressionMapping.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExpressionTrees.Task2.ExpressionMapping.Tests
{
    [TestClass]
    public class ExpressionMappingTests
    {
        // todo: add as many test methods as you wish, but they should be enough to cover basic scenarios of the mapping generator

        [TestMethod]
        public void MappingBetweenDifferentProperties()
        {
            var mappingGenerator = new MappingGenerator();
            var mapper = mappingGenerator.Generate<Bar, Foo>(
                new Dictionary<string, string>
                {
                    { "Id", "Guid" },
                    { "Income", "Salary" }
                }
            );

            var bar = new Bar { Id = Guid.NewGuid().ToString(), Income = 123.45M };
            var foo = mapper.Map(bar);

            Assert.AreEqual(bar.Id, foo.Guid);
            Assert.AreEqual(bar.Income, foo.Salary);
        }
    }
}
