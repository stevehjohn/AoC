using AoC.Solutions.Exceptions;

namespace AoC.Solutions.Solutions._2022._16;

public class Part2 : Base
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

        var queue = new PriorityQueue<(Valve Valve, int Time, Valve ElephantValve, int ElephantTime, int ReleasedPressure, int OpenedValves), int>();

        queue.Enqueue((Start, 26, Start, 26, 0, 0), 0);

        while (queue.Count > 0)
        {
            if (queue.Count % 100_000 == 0)
            {
                Console.WriteLine($"{max} ({queue.Count})");
            }

            var node = queue.Dequeue();

            if (node.Time <= 0 && node.ElephantTime <= 0)
            {
                if (node.ReleasedPressure > max)
                {
                    max = node.ReleasedPressure;

                    Console.WriteLine(max);
                }

                continue;
            }

            if (node.Valve.FlowRate > 0 && (node.OpenedValves & node.Valve.Designation) == 0 && node.Time > 0)
            {
                node.Time--;

                node.OpenedValves |= node.Valve.Designation;

                node.ReleasedPressure += node.Valve.FlowRate * node.Time;
            }

            if (node.ElephantValve.FlowRate > 0 && (node.OpenedValves & node.ElephantValve.Designation) == 0 && node.ElephantTime > 0)
            {
                node.ElephantTime--;

                node.OpenedValves |= node.ElephantValve.Designation;

                node.ReleasedPressure += node.ElephantValve.FlowRate * node.ElephantTime;
            }

            if (node.Time <= 0 && node.ElephantTime <= 0)
            {
                if (node.ReleasedPressure > max)
                {
                    max = node.ReleasedPressure;

                    Console.WriteLine(max);
                }

                continue;
            }

            foreach (var elephantValve in node.ElephantValve.WorkingValves)
            {
                foreach (var valve in node.Valve.WorkingValves)
                {
                    var isOpen = (node.OpenedValves & node.Valve.Designation) > 0;

                    var elephantOpen = (node.OpenedValves & elephantValve.Valve.Designation) > 0;

                    if (node.Time - valve.Cost < 0 && node.ElephantTime - elephantValve.Cost < 0)
                    {
                        continue;
                    }

                    var extraPressure = (node.Time - valve.Cost) * valve.Valve.FlowRate * (isOpen ? 0 : 1);

                    extraPressure += (node.ElephantTime - elephantValve.Cost) * elephantValve.Valve.FlowRate * (elephantOpen ? 0 : 1);

                    var totalPressure = node.ReleasedPressure + extraPressure;

                    var priority = 10_000;

                    priority -= totalPressure;

                    priority += isOpen || elephantOpen ? 20_000 : 0;

                    //priority += elephantOpen ? 20_000 : 0;

                    queue.Enqueue((valve.Valve, node.Time - valve.Cost, elephantValve.Valve, node.ElephantTime - elephantValve.Cost, node.ReleasedPressure, node.OpenedValves), priority);

                    if (queue.Count % 100_000 == 0)
                    {
                        Console.WriteLine($"{max} ({queue.Count})");
                    }
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