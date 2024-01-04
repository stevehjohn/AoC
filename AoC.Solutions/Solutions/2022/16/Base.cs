using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._16;

public abstract class Base : Solution
{
    public override string Description => "Proboscidea volcanium";

    private readonly List<Valve> _valves = new();

    private Valve _start;

    protected readonly Dictionary<int, (int Flow, int OpenedCount)> StateCache = new();

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
        var workingValves = _valves.Where(v => v.FlowRate > 0 || v.Name == "AA").ToList();

        var designation = 1;

        workingValves.ForEach(v =>
        {
            v.Designation = designation;
            designation <<= 1;
        });

        foreach (var valve in workingValves)
        {
            foreach (var connection in workingValves)
            {
                if (valve.Equals(connection) || connection.Name == "AA")
                {
                    continue;
                }

                valve.WorkingValves.Add((connection, GetSteps(valve, connection)));
            }
        }
    }
    
    protected void Traverse(int startMinutes)
    {
        var queue = new Queue<(Valve Valve, int Time, int Opened, int OpenedCount, int Flow)>();

        foreach (var valve in _start.WorkingValves)
        {
            queue.Enqueue((valve.Valve, startMinutes - valve.Cost, 0, 0, 0));
        }

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();

            if ((node.Opened & node.Valve.Designation) == 0)
            {
                node.Time--;

                node.Flow += node.Time * node.Valve.FlowRate;

                node.Opened |= node.Valve.Designation;

                node.OpenedCount++;

                StateCache[node.Opened] = (Math.Max(StateCache.GetValueOrDefault(node.Opened).Flow, node.Flow), node.OpenedCount);
            }

            if (node.Time <= 0)
            {
                continue;
            }

            foreach (var connection in node.Valve.WorkingValves)
            {
                var remaining = node.Time - connection.Cost;

                if (remaining <= 0 || (node.Opened & connection.Valve.Designation) != 0)
                {
                    continue;
                }

                queue.Enqueue((connection.Valve, remaining, node.Opened, node.OpenedCount, node.Flow));
            }
        }
    }

    private static int GetSteps(Valve start, Valve end)
    {
        var queue = new PriorityQueue<(Valve Valve, int Steps), int>();

        queue.Enqueue((start, 0), 0);

        var visited = new HashSet<int>();

        while (queue.Count > 0)
        {
            var item = queue.Dequeue();

            if (item.Valve.Equals(end))
            {
                return item.Steps;
            }

            foreach (var connection in item.Valve.DirectConnections)
            {
                var code = HashCode.Combine(connection.Name, item.Steps + 1);

                if (! visited.Contains(code))
                {
                    queue.Enqueue((connection, item.Steps + 1), item.Steps + 1);

                    visited.Add(code);
                }
            }
        }

        return -1;
    }
}