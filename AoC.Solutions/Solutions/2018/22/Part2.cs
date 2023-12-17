using AoC.Solutions.Common;
using AoC.Solutions.Exceptions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._22;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        GenerateMap(2);

        var result = Solve();

        return result.ToString();
    }

    private int Solve()
    {
        var queue = new PriorityQueue<(Point Position, char Equipped, int Cost), int>();

        queue.Enqueue((new Point(0, 0), 'T', 0), int.MaxValue);

        var visited = new HashSet<(Point, char)>();

        while (queue.Count > 0)
        {
            var state = queue.Dequeue();

            if (state.Position.X == TargetX && state.Position.Y == TargetY)
            {
                var cost = state.Cost;

                if (state.Equipped != 'T')
                {
                    cost += 7;
                }

                return cost;
            }

            if (! visited.Add((state.Position, state.Equipped)))
            {
                continue;
            }

            foreach (var neighbor in GetNeighbors(state.Position))
            {
                var cost = state.Cost + 1;

                var equipped = state.Equipped;

                var options = new List<char>();

                switch (Map[neighbor.X, neighbor.Y])
                {
                    case '.':
                        if (equipped is 'C' or 'T')
                        {
                            break;
                        }

                        options.Add('C');

                        options.Add('T');

                        cost += 7;

                        break;
                    case '=':
                        if (equipped is 'C' or ' ')
                        {
                            break;
                        }

                        options.Add('C');

                        options.Add(' ');

                        cost += 7;

                        break;
                    case '|':
                        if (equipped is 'T' or ' ')
                        {
                            break;
                        }

                        options.Add('T');

                        options.Add(' ');

                        cost += 7;

                        break;
                }

                if (options.Count == 0)
                {
                    queue.Enqueue((neighbor, equipped, cost), cost);

                    continue;
                }

                foreach (var option in options)
                {
                    queue.Enqueue((neighbor, option, cost), cost);
                }
            }
        }

        throw new PuzzleException("Solution not found.");
    }

    private List<Point> GetNeighbors(Point position)
    {
        var neighbors = new List<Point>();

        if (position.X > 0)
        {
            neighbors.Add(new Point(position.X - 1, position.Y));
        }

        if (position.X < Width - 1)
        {
            neighbors.Add(new Point(position.X + 1, position.Y));
        }

        if (position.Y > 0)
        {
            neighbors.Add(new Point(position.X, position.Y - 1));
        }

        if (position.Y < Height - 1)
        {
            neighbors.Add(new Point(position.X, position.Y + 1));
        }

        return neighbors;
    }
}