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

        var added = new HashSet<(int ValveDesignation, int Time, int OpenedValves)>();

        var queue = new Queue<(Valve Valve, int Time, int ReleasedPressure, int OpenedValves, List<(string ValveName, int Time)> History)>();

        var paths = new List<List<(string ValveName, int Flow)>>();

        foreach (var valve in Start.WorkingValves)
        {
            queue.Enqueue((valve.Valve, 26 - valve.Cost, 0, 0, new List<(string ValveName, int Time)>()));
        }

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();

            var history = new List<(string ValveName, int Time)>(node.History);

            if ((node.OpenedValves & node.Valve.Designation) == 0)
            {
                node.Time--;

                node.OpenedValves |= node.Valve.Designation;

                node.ReleasedPressure += node.Valve.FlowRate * node.Time;

                history.Add((node.Valve.Name, node.Time * node.Valve.FlowRate));
            }

            if (node.ReleasedPressure > max)
            {
                max = node.ReleasedPressure;
            }

            if (node.Time <= 0)
            {
                paths.Add(node.History);

                continue;
            }

            foreach (var valve in node.Valve.WorkingValves)
            {
                if (node.Time - valve.Cost <= 0 || (valve.Valve.Designation & node.OpenedValves) > 0)
                {
                    continue;
                }

                var newItem = (valve.Valve.Designation, node.Time, node.OpenedValves);

                if (! added.Contains(newItem))
                {
                    queue.Enqueue((valve.Valve, node.Time - valve.Cost, node.ReleasedPressure, node.OpenedValves, history));

                    added.Add(newItem);
                }
            }
        }

        var ordered = paths.Where(p => p.Count > 5).ToList();

        var m = 0;

        foreach (var path in ordered)
        {
            var other = ordered.Where(o => ! o.Any(ov => path.Any(pv => pv.ValveName == ov.ValveName))).ToList();

            if (other.Count > 0)
            {
                foreach (var o in other)
                {
                    var sum = path.Sum(p => p.Flow) + o.Sum(k => k.Flow);

                    if (sum > m)
                    {
                        m = sum;
                    }

                    Console.WriteLine(path.Sum(p => p.Flow) + o.Sum(k => k.Flow));
                }
            }
        }

        Console.WriteLine();
        
        Console.WriteLine(m);

        return 0;
    }
}