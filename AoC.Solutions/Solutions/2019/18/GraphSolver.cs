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

                // TODO: This may need a + 1 after the loop? Actually, should probably be 30 after all
                target += _graphs[i].Nodes.Count - 1;
            }
        }

        var vc = 0;

        while (queue.Count > 0)
        {
            var walker = queue.Dequeue();

            //Console.WriteLine(walker.Signature);

            if (walker.Visited.Count >= vc)
            {
                foreach (var c in walker.Visited)
                {
                    Console.Write(c);
                }

                Console.WriteLine();

                vc = walker.Visited.Count;
            }

            var newWalkers = walker.Walk();

            if (newWalkers.Count == 0)
            {
                if (walker.VisitedCount == target)
                {
                    var pathBuilder = new StringBuilder();

                    foreach (var c in walker.Visited)
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

                queue.Enqueue(newWalker, (newWalker.Steps + 1) * (newWalker.IsGraphSwitch ? 1000 : 1));
            }
        }

        throw new PuzzleException("No solution found.");
    }
}