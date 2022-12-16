namespace AoC.Solutions.Solutions._2022._16;

public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        OptimiseGraph();

        Solve();

        return "";
    }

    private int _max;

    private void Solve()
    {
        var queue = new PriorityQueue<(Valve Valve, int Time, int ReleasedPressure, List<string> OpenedValves, List<string> History), float>();

        queue.Enqueue((Start, 30, 0, new(), new() { Start.Name }), 0);

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();

            if (node.Time <= 0)
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

            if (node.Valve.FlowRate > 0 && !node.OpenedValves.Contains(node.Valve.Name))
            {
                node.Time--;

                node.OpenedValves.Add(node.Valve.Name);

                node.ReleasedPressure += node.Valve.FlowRate * node.Time;

                node.History.Add("O");
            }

            if (node.Time <= 0)
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
                if (node.Time - valve.Cost >= 0)
                {
                    queue.Enqueue((valve.Valve, node.Time - valve.Cost, node.ReleasedPressure, node.OpenedValves.ToList(), new List<string>(node.History) { valve.Valve.Name }),
                                  100 - (float)valve.Valve.FlowRate / valve.Cost + (node.OpenedValves.Contains(valve.Valve.Name) ? 50 : 0));
                }
            }
        }
    }
}