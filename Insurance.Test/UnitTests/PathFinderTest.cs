using FluentAssertions;
using Insurance.Domain.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace Insurance.Test.UnitTests
{
    public class PathFinderTest
    {
        [Fact]
        public void FindShortestPathValid()
        {
            // arrange
            var a = Guid.NewGuid();
            var b = Guid.NewGuid();
            var c = Guid.NewGuid();
            var d = Guid.NewGuid();

            var vertices = new Guid[] { a, b, c, d };
            var edges = new List<Guid[]> { new Guid[] { a, b }, new Guid[] { b, c }, new Guid[] { c, d } };

            // act
            var finder = new PathFinderService();
            var results = finder.FindShortestPath(vertices, edges, vertices[0], vertices[3]);

            // assertation
            var resultsExpected = new Guid[] { a, b, c, d };
            results.Should().BeEquivalentTo(resultsExpected);
        }

        [Fact]
        public void FindShortestPathValid2()
        {
            // arrange
            var a = Guid.NewGuid();
            var b = Guid.NewGuid();
            var c = Guid.NewGuid();
            var d = Guid.NewGuid();

            var vertices = new Guid[] { a, b, c, d };
            var edges = new List<Guid[]> { new Guid[] { a, b }, new Guid[] { b, c }, new Guid[] { c, d }, new Guid[] { b, d } };

            // act
            var finder = new PathFinderService();
            var results = finder.FindShortestPath(vertices, edges, vertices[0], vertices[3]);

            // assertation
            var resultsExpected = new Guid[] { a, b, d };
            results.Should().BeEquivalentTo(resultsExpected);
        }
    }
}