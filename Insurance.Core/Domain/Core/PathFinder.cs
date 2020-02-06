using System;
using System.Collections.Generic;
using System.Linq;

namespace Insurance.Core.Domain.Core
{
    public class PathFinder : IPathFinder
    {
        private readonly Dictionary<Guid, Node> _nodes = new Dictionary<Guid, Node>();
        private readonly List<Node> _visiteds = new List<Node>();
        private readonly Dictionary<Node, Level> _levels = new Dictionary<Node, Level>();
        private readonly List<Node> _scheduled = new List<Node>();

        public PathFinder()
        {
        }

        public Guid[] FindShortestPath(Guid[] vertices, List<Guid[]> edges, Guid from, Guid to)
        {
            Node nodeFrom = null;
            Node nodeTo = null;

            foreach (var id in vertices)
            {
                var node = new Node(id);
                _nodes.Add(id, node);

                if (id == from)
                    nodeFrom = node;

                if (id == to)
                    nodeTo = node;
            }

            foreach (var item in edges)
            {
                var node = _nodes[item[0]];
                node.ConnectTo(_nodes[item[1]]);
            }

            return FindShortestPath(nodeFrom, nodeTo).Select(x => x.Id).ToArray();
        }

        private Node[] FindShortestPath(Node @from, Node to)
        {
            SetLevel(@from, new Level(null, 0));

            _scheduled.Add(@from);

            while (_scheduled.Count > 0)
            {
                var visitingNode = GetNodeToVisit();
                var visitingNodeWeight = GetLevel(visitingNode);
                SetVisitTo(visitingNode);

                foreach (var neighborhoodInfo in visitingNode.Neighbors)
                {
                    if (!WasVisited(neighborhoodInfo.Node))
                    {
                        _scheduled.Add(neighborhoodInfo.Node);
                    }

                    var neighborWeight = GetLevel(neighborhoodInfo.Node);

                    var probableWeight = (visitingNodeWeight.Value + neighborhoodInfo.WeightToNode);
                    if (neighborWeight.Value > probableWeight)
                    {
                        SetLevel(neighborhoodInfo.Node, new Level(visitingNode, probableWeight));
                    }
                }
            }

            return HasPathToOrigin(to)
                ? PathToOrigin(to).Reverse().ToArray()
                : Enumerable.Empty<Node>().ToArray();
        }

        private void SetLevel(Node node, Level newWeight)
        {
            if (!_levels.ContainsKey(node))
            {
                _levels.Add(node, newWeight);
            }
            else
            {
                _levels[node] = newWeight;
            }
        }

        private Node GetNodeToVisit()
        {
            var ordered = from n in _scheduled
                          orderby GetLevel(n).Value
                          select n;

            var result = ordered.First();
            _scheduled.Remove(result);
            return result;
        }

        private Level GetLevel(Node node)
        {
            Level result;
            if (!_levels.ContainsKey(node))
            {
                result = new Level(null, int.MaxValue);
                _levels.Add(node, result);
            }
            else
            {
                result = _levels[node];
            }
            return result;
        }

        private void SetVisitTo(Node node)
        {
            if (!_visiteds.Contains(node))
                _visiteds.Add(node);
        }

        private bool WasVisited(Node node)
        {
            return _visiteds.Contains(node);
        }

        private bool HasPathToOrigin(Node node)
        {
            return GetLevel(node).From != null;
        }

        private IEnumerable<Node> PathToOrigin(Node node)
        {
            var n = node;
            while (n != null)
            {
                yield return n;
                n = GetLevel(n).From;
            }
        }

        private class Node
        {
            private readonly List<Edge> _edges = new List<Edge>();

            public Node(Guid id)
            {
                Id = id;
            }

            public Guid Id { get; }

            public IEnumerable<NeighborhoodInfo> Neighbors =>
                from edge in _edges
                select new NeighborhoodInfo(
                    edge.Node1 == this ? edge.Node2 : edge.Node1,
                    edge.Value
                    );

            private void Assign(Edge edge)
            {
                _edges.Add(edge);
            }

            public void ConnectTo(Node other)
            {
                Edge.Create(this, other);
            }

            public struct NeighborhoodInfo
            {
                public Node Node { get; }
                public int WeightToNode { get; }

                public NeighborhoodInfo(Node node, int weightToNode)
                {
                    Node = node;
                    WeightToNode = weightToNode;
                }
            }

            public class Edge
            {
                public int Value { get; }
                public Node Node1 { get; }
                public Node Node2 { get; }

                public Edge(Node node1, Node node2)
                {
                    Value = 1;
                    Node1 = node1;
                    Node2 = node2;
                }

                public static void Create(Node node1, Node node2)
                {
                    var e = new Edge(node1, node2);
                    node1.Assign(e);
                    node2.Assign(e);
                }
            }
        }

        private class Level
        {
            public Node From { get; }
            public int Value { get; }

            public Level(Node @from, int value)
            {
                From = @from;
                Value = value;
            }
        }
    }
}