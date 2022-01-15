﻿using System.Diagnostics;

namespace AoC.Solutions.Solutions._2019._18;

public class Graph
{
    private readonly Dictionary<char, Node> _nodes = new();

    private Dictionary<string, string> _doors;

    private Dictionary<string, int> _distances;

    public void Build(Dictionary<string, int> distances, Dictionary<string, string> doors)
    {
        _distances = distances;

        _doors = doors;

        _nodes.Add('@', new Node('@'));

        foreach (var c in _distances.Select(d => char.ToLower(d.Key[1])).Distinct())
        {
            _nodes.Add(c, new Node(c));
        }

        foreach (var (parentKey, node) in _nodes)
        {
            var connections = _distances.Where(d => d.Key.Contains(parentKey));

            foreach (var (childKey, distance) in connections)
            {
                var child = childKey.Replace(parentKey.ToString(), string.Empty)[0];

                if (char.IsUpper(child))
                {
                    continue;
                }

                node.Children.Add(_nodes[child], distance);
            }
        }
    }

    public int Solve()
    {
        var queue = new PriorityQueue<NodeWalker, int>();

        queue.Enqueue(new NodeWalker(_nodes['@'], _doors), 0);

        var minSteps = int.MaxValue;

        var sw = Stopwatch.StartNew();

        while (queue.Count > 0)
        {
            var walker = queue.Dequeue();

            if (walker.Steps >= minSteps)
            {
                continue;
            }

            var newWalkers = walker.Walk();

            if (newWalkers.Count == 0 && walker.VisitedCount == _nodes.Count)
            {
                var total = 0;

                var prev = '\0';

                foreach (var c in walker.Visited)
                {
                    if (prev != '\0')
                    {
                        var pair = new string(new[] { prev, c }.OrderBy(x => x).ToArray());

                        var steps = _distances[pair];

                        Console.WriteLine($"{prev} -> {c}: {steps}");

                        total += steps;
                    }

                    prev = c;
                }

                Console.WriteLine();
                
                Console.WriteLine($"{total} vs {walker.Steps}");
                
                Console.WriteLine();

                if (walker.Steps < minSteps)
                {
                    minSteps = walker.Steps;
                }

                continue;
            }

            newWalkers.ForEach(w => queue.Enqueue(w, int.MaxValue - w.VisitedCount));
        }

        sw.Stop();

        Console.WriteLine(sw.Elapsed);

        return minSteps;
    }
}