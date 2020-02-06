using System;
using System.Collections.Generic;

namespace Insurance.Core.Domain.Core
{
    public interface IPathFinder
    {
        Guid[] FindShortestPath(Guid[] vertices, List<Guid[]> edges, Guid from, Guid to);
    }
}