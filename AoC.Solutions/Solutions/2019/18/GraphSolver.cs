using System.Text;
using AoC.Solutions.Exceptions;

namespace AoC.Solutions.Solutions._2019._18;

public class GraphSolver
{
    private readonly Graph[] _graphs;

    public GraphSolver(Graph[] graphs)
    {
        _graphs = graphs;
    }

    public (int Steps, string Path) Solve()
    {
        var queue = new PriorityQueue<INodeWalker, int>();

        var signatures = new Dictionary<string, int>();

        var target = 0;

        if (_graphs.Length == 1)
        {
            var startWalker = new NodeWalker(_graphs[0].Nodes['@'], _graphs[0]);

            signatures.Add(startWalker.Signature, 0);

            queue.Enqueue(startWalker, 0);

            target = _graphs[0].Nodes.Count;
        }
        else
        {
            for (var i = 0; i < _graphs.Length; i++)
            {
                if (i == 2)
                {
                    var startWalker = new MultiGraphNodeWalker(_graphs, i);

                    signatures.Add(startWalker.Signature, 0);

                    queue.Enqueue(startWalker, 0);
                }

                target += _graphs[i].Nodes.Count;
            }
        }

        while (queue.Count > 0)
        {
            var walker = queue.Dequeue();

            var newWalkers = walker.Walk();

            if (newWalkers.Count == 0 || _graphs.Length > 1)
            {
                if (walker.VisitedCount == target)
                {
                    var pathBuilder = new StringBuilder();

                    foreach (var c in walker.AllVisited)
                    {
                        pathBuilder.Append(c);
                    }

                    var path = pathBuilder.ToString();

                    return (walker.Steps, Path: path);
                }
            }

            foreach (var newWalker in newWalkers)
            {
                var signature = newWalker.Signature;

                if (signatures.ContainsKey(signature))
                {
                    if (signatures[signature] <= newWalker.Steps)
                    {
                        continue;
                    }

                    signatures[signature] = newWalker.Steps;
                }
                else
                {
                    signatures.Add(signature, newWalker.Steps);
                }

                queue.Enqueue(newWalker, newWalker.Steps);
            }
        }

        throw new PuzzleException("No solution found.");
    }
}