using System.Text;

namespace AoC.Solutions.Solutions._2019._18;

public class GraphSolver
{
    private readonly Graph _graph;

    public GraphSolver(Graph graph)
    {
        _graph = graph;
    }

    public (int Steps, string Path) Solve()
    {
        var queue = new PriorityQueue<NodeWalker, int>();

        var signatures = new Dictionary<string, int>();

        var startWalker = new NodeWalker(_graph.Nodes['@'], _graph);

        signatures.Add(startWalker.Signature, 0);

        queue.Enqueue(startWalker, 0);

        var minSteps = int.MaxValue;

        var path = string.Empty;

        while (queue.Count > 0)
        {
            var walker = queue.Dequeue();

            if (walker.Steps >= minSteps)
            {
                continue;
            }

            var newWalkers = walker.Walk();

            if (newWalkers.Count == 0 && walker.VisitedCount == _graph.Nodes.Count)
            {
                if (walker.Steps < minSteps)
                {
                    var pathBuilder = new StringBuilder();

                    foreach (var c in walker.Visited)
                    {
                        pathBuilder.Append(c);
                    }

                    path = pathBuilder.ToString();

                    minSteps = walker.Steps;
                }

                continue;
            }

            foreach (var newWalker in newWalkers)
            {
                var signature = newWalker.Signature;

                if (signatures.ContainsKey(signature))
                {
                    if (signatures[signature] < newWalker.Steps)
                    {
                        continue;
                    }
                }
                else
                {
                    signatures.Add(signature, newWalker.Steps);
                }

                queue.Enqueue(newWalker, walker.Steps);
            }
        }

        return (Steps: minSteps, Path: path);
    }
}