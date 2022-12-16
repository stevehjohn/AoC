using AoC.Solutions.Exceptions;

namespace AoC.Solutions.Solutions._2022._16;

public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        OptimiseGraph();

        return Solve().ToString();
    }

    private int Solve()
    {
        var max = 0;

        var queue = new PriorityQueue<(Valve Valve, int Time, int ReleasedPressure, List<string> OpenedValves, List<string> History), int>();

        queue.Enqueue((Start, 30, 0, new(), new() { Start.Name }), 0);

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();

            if (node.Time <= 0 && node.OpenedValves.Capacity < WorkingValves)
            {
                continue;
            }

            if (node.Time <= 0)
            {
                if (node.ReleasedPressure > max)
                {
                    max = node.ReleasedPressure;

                    Console.WriteLine(max);

                    node.History.ForEach(h => Console.Write($"{h} -> "));

                    Console.WriteLine();
                }

                continue;
            }

            if (node.Valve.FlowRate > 0 && ! node.OpenedValves.Contains(node.Valve.Name))
            {
                node.Time--;

                node.OpenedValves.Add(node.Valve.Name);

                node.ReleasedPressure += node.Valve.FlowRate * node.Time;

                node.History.Add("O");

                if (node.OpenedValves.Count == WorkingValves)
                {
                    if (node.ReleasedPressure > max)
                    {
                        max = node.ReleasedPressure;

                        Console.WriteLine(max);

                        node.History.ForEach(h => Console.Write($"{h} -> "));

                        Console.WriteLine();
                    }

                    continue;
                }
            }

            foreach (var valve in node.Valve.WorkingValves)
            {
                var priority = 1_000 - (valve.Valve.FlowRate - valve.Cost);

                priority += node.OpenedValves.Contains(valve.Valve.Name) ? 1_000 : 0;

                queue.Enqueue((valve.Valve, node.Time - valve.Cost, node.ReleasedPressure, node.OpenedValves.ToList(), new List<string>(node.History) { $"{valve.Valve.Name}: ({valve.Valve.FlowRate}, {valve.Cost}) {priority}" }), priority);
            }
        }

        throw new PuzzleException("Solution not found");
    }
}