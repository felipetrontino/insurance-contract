using System;
using System.Collections.Generic;

namespace Insurance.Domain.Interfaces.Service
{
    public interface IPathFinderService
    {
        Guid[] FindShortestPath(Guid[] vertices, List<Guid[]> edges, Guid from, Guid to);
    }
}