using AoC.Solutions.Exceptions;

namespace AoC.Solutions.Solutions._2022._16;

public class Part2 : Base
{
    private const int StartMinutes = 26;

    private const int ArbitrarySize = 110_000_000;

    public override string GetAnswer()
    {
        ParseInput();

        OptimiseGraph();

        return Solve().ToString();
    }

    private int Solve()
    {
        var max = 0;

        var added = new HashSet<int>(ArbitrarySize);

        var count = 1;

        var readPosition = 0;

        var addPosition = 1;

        var queue = new (Valve Valve, int Time, Valve ElephantValve, int ElephantTime, int ReleasedPressure, int OpenedValves, int AvailableTotalFlow)[ArbitrarySize];

        var availableTotalFlow = Start.WorkingValves.Sum(v => v.Valve.FlowRate);

        queue[0] = (Start, StartMinutes, Start, StartMinutes, 0, 0, availableTotalFlow);

        while (count > 0)
        {
            var node = queue[readPosition];

            readPosition++;

            count--;

            if (node.Valve.FlowRate > 0 && (node.OpenedValves & node.Valve.Designation) == 0 && node.Time > 0)
            {
                node.Time--;

                node.OpenedValves |= node.Valve.Designation;

                node.ReleasedPressure += node.Valve.FlowRate * node.Time;

                node.AvailableTotalFlow -= node.Valve.FlowRate;
            }

            if (node.ReleasedPressure > max)
            {
                max = node.ReleasedPressure;
            }

            if (node.Time <= 0 && node.ElephantTime <= 0)
            {
                continue;
            }

            if (node.ElephantValve.FlowRate > 0 && (node.OpenedValves & node.ElephantValve.Designation) == 0 && node.ElephantTime > 0)
            {
                node.ElephantTime--;

                node.OpenedValves |= node.ElephantValve.Designation;

                node.ReleasedPressure += node.ElephantValve.FlowRate * node.ElephantTime;

                node.AvailableTotalFlow -= node.Valve.FlowRate;
            }

            if (node.ReleasedPressure > max)
            {
                max = node.ReleasedPressure;
            }

            if (node.Time <= 0 && node.ElephantTime <= 0)
            {
                continue;
            }

            foreach (var elephantValve in node.ElephantValve.WorkingValves)
            {
                foreach (var valve in node.Valve.WorkingValves)
                {
                    if ((valve.Valve.Designation & elephantValve.Valve.Designation) > 0)
                    {
                        continue;
                    }

                    if ((valve.Valve.Designation & node.OpenedValves) > 0 && (elephantValve.Valve.Designation & node.OpenedValves) > 0)
                    {
                        continue;
                    }

                    if (valve.Equals(elephantValve))
                    {
                        continue;
                    }

                    if (node.Time - valve.Cost < 0 && node.ElephantTime - elephantValve.Cost < 0)
                    {
                        continue;
                    }

                    if (node.ReleasedPressure + node.AvailableTotalFlow * node.ElephantTime * node.Time < max)
                    {
                        continue;
                    }

                    var hash = new HashCode();

                    hash.Add(valve.Valve);
                    hash.Add(node.Time + node.ElephantTime);
                    hash.Add(elephantValve.Valve);
                    hash.Add(node.OpenedValves);

                    var code = hash.ToHashCode();

                    if (! added.Contains(code))
                    {
                        queue[addPosition] = (valve.Valve, node.Time - valve.Cost, elephantValve.Valve, node.ElephantTime - elephantValve.Cost, node.ReleasedPressure, node.OpenedValves, node.AvailableTotalFlow);
                        
                        addPosition++;

                        count++;

                        added.Add(code);
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