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

        var queue = new PriorityQueue<(Valve Valve, int Time, int ReleasedPressure, int OpenedValves), int>();

        queue.Enqueue((Start, 30, 0, new()), 0);

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();

            if (node.Time <= 0)
            {
                if (node.ReleasedPressure > max)
                {
                    max = node.ReleasedPressure;
                }

                continue;
            }

            if (node.Valve.FlowRate > 0 && (node.OpenedValves & node.Valve.Designation) == 0)
            {
                node.Time--;

                node.OpenedValves |=node.Valve.Designation;

                node.ReleasedPressure += node.Valve.FlowRate * node.Time;
            }

            if (node.Time <= 0)
            {
                if (node.ReleasedPressure > max)
                {
                    max = node.ReleasedPressure;
                }

                continue;
            }

            foreach (var valve in node.Valve.WorkingValves)
            {
                if (node.Time - valve.Cost < 0)
                {
                    continue;
                }

                var isOpen = (node.OpenedValves & node.Valve.Designation) > 0;

                var extraPressure = (node.Time - valve.Cost) * valve.Valve.FlowRate * (isOpen ? 0 : 1);

                var totalPressure = node.ReleasedPressure + extraPressure;

                var priority = 10_000;

                priority -= totalPressure;

                priority += isOpen ? 20_000 : 0;

                queue.Enqueue((valve.Valve, node.Time - valve.Cost, node.ReleasedPressure, node.OpenedValves), priority);
            }
        }

        if (max == 0)
        {
            throw new PuzzleException("Solution not found");
        }
     
        return max;
    }
}