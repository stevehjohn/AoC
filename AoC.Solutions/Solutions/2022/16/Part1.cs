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

            if (node.Time <= 0)
            {
                if (node.ReleasedPressure > max)
                {
                    max = node.ReleasedPressure;

                    Console.WriteLine(max);

                    node.History.ForEach(h => Console.Write($"{h} -> "));

                    Console.WriteLine("\n");
                }

                continue;
            }

            if (node.Valve.FlowRate > 0 && ! node.OpenedValves.Contains(node.Valve.Name))
            {
                node.Time--;

                node.OpenedValves.Add(node.Valve.Name);

                node.ReleasedPressure += node.Valve.FlowRate * node.Time;

                //node.History.Add("O");
            }

            if (node.Time <= 0)
            {
                if (node.ReleasedPressure > max)
                {
                    max = node.ReleasedPressure;

                    Console.WriteLine(max);

                    node.History.ForEach(h => Console.Write($"{h} -> "));

                    Console.WriteLine("\n");
                }

                continue;
            }

            foreach (var valve in node.Valve.WorkingValves)
            {
                var priority = 10_000;

                if (node.Time - valve.Cost < 0)
                {
                    continue;
                }

                var extraPressure = (node.Time - valve.Cost) * valve.Valve.FlowRate * (node.OpenedValves.Contains(valve.Valve.Name) ? 0 : 1);

                priority -= node.ReleasedPressure + extraPressure;

                priority += node.OpenedValves.Contains(valve.Valve.Name) ? 20_000 : 0;

                //Console.WriteLine($"{valve.Valve.Name}: ({valve.Valve.FlowRate}, {valve.Cost}) {priority}");

                //var history = $"{valve.Valve.Name}: ({valve.Valve.FlowRate}, {valve.Cost}) {priority}";
                var history = $"{valve.Valve.Name}: {node.ReleasedPressure + (node.Time - valve.Cost) * valve.Valve.FlowRate * (node.OpenedValves.Contains(valve.Valve.Name) ? 0 : 1)} ({valve.Cost}, {valve.Valve.FlowRate}): {priority})";

                queue.Enqueue((valve.Valve, node.Time - valve.Cost, node.ReleasedPressure, node.OpenedValves.ToList(), new List<string>(node.History) { history }), priority);
            }
        }

        if (max == 0)
        {
            throw new PuzzleException("Solution not found");
        }
     
        return max;
    }
}