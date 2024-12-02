using AoC.Solutions.Common;
using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2016._13;

public abstract class Base : Solution
{
    public override string Description => "Cubicle maze";

    private bool[,] _maze;

    protected int Width;

    protected int Height;

    protected void BuildMaze()
    {
        _maze = new bool[Width, Height];

        var designerNumber = int.Parse(Input[0]);

        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                var number = x * x + 3 * x + 2 * x * y + y + y * y;

                number += designerNumber;

                var bits = number.CountBits();

                if (bits % 2 == 1)
                {
                    _maze[x, y] = true;
                }
            }
        }
    }

    protected int Solve(int destinationX = 0, int destinationY = 0)
    {
        var queue = new PriorityQueue<(Point Position, int Steps), int>();

        queue.Enqueue((new Point(1, 1), 0), 0);

        var visited = new HashSet<Point> { new(1, 1) };

        var steps = 0;

        var locations = 0;

        while (queue.Count > 0)
        {
            var (position, i) = queue.Dequeue();

            if (destinationX > 0)
            {
                if (position.X == destinationX && position.Y == destinationY)
                {
                    steps = i;

                    break;
                }
            }
            else if (i <= 50)
            {
                locations++;
            }

            if (position.Y > 0 && ! _maze[position.X, position.Y - 1] && ! visited.Contains(new Point(position.X, position.Y - 1)))
            {
                queue.Enqueue((new Point(position.X, position.Y - 1), i + 1), i + 1);

                visited.Add(new Point(position.X, position.Y - 1));
            }

            if (position.X < Width - 1 && ! _maze[position.X + 1, position.Y] && ! visited.Contains(new Point(position.X + 1, position.Y)))
            {
                queue.Enqueue((new Point(position.X + 1, position.Y), i + 1), i + 1);

                visited.Add(new Point(position.X + 1, position.Y));
            }

            if (position.Y < Height - 1 && ! _maze[position.X, position.Y + 1] && ! visited.Contains(new Point(position.X, position.Y + 1)))
            {
                queue.Enqueue((new Point(position.X, position.Y + 1), i + 1), i + 1);

                visited.Add(new Point(position.X, position.Y + 1));
            }

            if (position.X > 0 && ! _maze[position.X - 1, position.Y] && ! visited.Contains(new Point(position.X - 1, position.Y)))
            {
                queue.Enqueue((new Point(position.X - 1, position.Y), i + 1), i + 1);

                visited.Add(new Point(position.X - 1, position.Y));
            }
        }

        return destinationX > 0 ? steps : locations;
    }
}