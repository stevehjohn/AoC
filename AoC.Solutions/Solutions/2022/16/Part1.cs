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

        var added = new HashSet<int>();

        var queue = new Queue<(Valve Valve, int Time, int ReleasedPressure, int OpenedValves)>();

        foreach (var valve in Start.WorkingValves)
        {
            queue.Enqueue((valve.Valve, 30 - valve.Cost, 0, 0));
        }

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();

            if ((node.OpenedValves & node.Valve.Designation) == 0)
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

                var hash = new HashCode();

                hash.Add(valve.Valve);
                hash.Add(node.Time);
                hash.Add(node.OpenedValves);

                var code = hash.ToHashCode();

                if (! added.Contains(code))
                {
                    queue.Enqueue((valve.Valve, node.Time - valve.Cost, node.ReleasedPressure, node.OpenedValves));

                    added.Add(code);
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