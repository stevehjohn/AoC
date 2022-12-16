using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._16;

public abstract class Base : Solution
{
    public override string Description => "Proboscidea volcanium";

    private readonly List<Valve> _valves = new();

    private Valve _start;

    protected void ParseInput()
    {
        var connections = new Dictionary<string, string[]>();

        foreach (var line in Input)
        {
            var name = line[6..8];

            var parts = line.Split(';', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            var flowRate = int.Parse(parts[0][23..]);

            var valve = new Valve(name, flowRate);

            _valves.Add(valve);

            if (name == "AA")
            {
                _start = valve;
            }

            var connectedValves = parts[1][22..].Trim().Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            connections.Add(name, connectedValves);
        }

        foreach (var valve in _valves)
        {
            valve.DirectConnections.AddRange(_valves.Where(v => connections[valve.Name].Contains(v.Name)));
        }
    }

    protected void OptimiseGraph()
    {
        var workingValves = _valves.Where(v => v.FlowRate > 0).ToList();

        foreach (var valve in _valves)
        {
            foreach (var connection in workingValves)
            {
                if (valve.Equals(connection))
                {
                    continue;
                }

                valve.WorkingValves.Add((connection, GetSteps(valve, connection)));
            }
        }
    }

    private int _max;

    protected void Solve()
    {
        var queue = new PriorityQueue<(Valve Valve, int Time, int ReleasedPressure, List<string> OpenedValves, List<string> History), float>();

        queue.Enqueue((_start, 30, 0, new(), new() { _start.Name }), 0);

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();

            if (node.Time == 0)
            {
                if (node.ReleasedPressure > _max)
                {
                    _max = node.ReleasedPressure;

                    Console.WriteLine(_max);

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
            }

            if (node.Time == 0)
            {
                if (node.ReleasedPressure > _max)
                {
                    _max = node.ReleasedPressure;

                    Console.WriteLine(_max);

                    node.History.ForEach(h => Console.Write($"{h} -> "));

                    Console.WriteLine();
                }

                continue;
            }

            foreach (var valve in node.Valve.WorkingValves)
            {
                queue.Enqueue((valve.Valve, node.Time - valve.Cost, node.ReleasedPressure, node.OpenedValves.ToList(), new List<string>(node.History) { valve.Valve.Name }), 100 - (float) valve.Valve.FlowRate / valve.Cost + (node.OpenedValves.Contains(valve.Valve.Name) ? 50 : 0));
            }
        }
    }

    private static int GetSteps(Valve start, Valve end)
    {
        var queue = new PriorityQueue<(Valve Valve, int Steps), int>();

        queue.Enqueue((start, 0), 0);

        while (queue.Count > 0)
        {
            var item = queue.Dequeue();

            if (item.Valve.Equals(end))
            {
                return item.Steps;
            }

            foreach (var connection in item.Valve.DirectConnections)
            {
                queue.Enqueue((connection, item.Steps + 1), item.Steps + 1);
            }
        }

        return -1;
    }
}