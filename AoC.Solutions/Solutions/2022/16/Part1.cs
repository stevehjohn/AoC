using AoC.Solutions.Exceptions;

namespace AoC.Solutions.Solutions._2022._16;

public class Part1 : Base
{
    private readonly HashSet<(Valve Valve, int Time, int OpenedValves)> _added = new();

    public override string GetAnswer()
    {
        ParseInput();

        OptimiseGraph();

        return Solve().ToString();
    }

    private int Solve()
    {
        var max = 0;

        var queue = new Queue<(Valve Valve, int Time, int ReleasedPressure, int OpenedValves)>();

        queue.Enqueue((Start, 30, 0, 0));

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();

            if (node.Valve.FlowRate > 0 && (node.OpenedValves & node.Valve.Designation) == 0)
            {
                node.Time--;

                node.OpenedValves |= node.Valve.Designation;

                node.ReleasedPressure += node.Valve.FlowRate * node.Time;
            }

            if (node.ReleasedPressure > max)
            {
                max = node.ReleasedPressure;
            }

            if (node.Time <= 0)
            {
                continue;
            }

            foreach (var valve in node.Valve.WorkingValves)
            {
                if (node.Time - valve.Cost <= 0)
                {
                    continue;
                }

                var newItem = (valve.Valve, node.Time, node.OpenedValves);

                if (! _added.Contains(newItem))
                {
                    queue.Enqueue((valve.Valve, node.Time - valve.Cost, node.ReleasedPressure, node.OpenedValves));

                    _added.Add(newItem);
                }
            }
        }

        if (max == 0)
        {
            throw new PuzzleException("Solution not found");
        }

        return max;
    }
}