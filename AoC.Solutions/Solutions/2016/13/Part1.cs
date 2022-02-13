using AoC.Solutions.Common;
using AoC.Solutions.Extensions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._13;

[UsedImplicitly]
public class Part1 : Base
{
    private bool[,] _maze;

    private int _width;

    private int _height;

    public override string GetAnswer()
    {
        BuildMaze(31, 39);

        var result = Solve(31, 39);

        return result.ToString();
    }

    private int Solve(int destinationX, int destinationY)
    {
        var queue = new PriorityQueue<(Point Position, int Steps), int>();

        queue.Enqueue((new Point(1, 1), 0), 0);

        var visited = new HashSet<Point>();

        var steps = 0;

        while (queue.Count > 0)
        {
            var item = queue.Dequeue();

            var position = item.Position;

            if (position.X == destinationX && position.Y == destinationY)
            {
                steps = item.Steps;

                break;
            }

            if (position.Y > 0 && ! _maze[position.X, position.Y - 1] && ! visited.Contains(new Point(position.X, position.Y - 1)))
            {
                queue.Enqueue((new Point(position.X, position.Y - 1), item.Steps + 1), item.Steps + 1);

                visited.Add(new Point(position.X, position.Y - 1));
            }

            if (position.X < _width - 1 && ! _maze[position.X + 1, position.Y] && ! visited.Contains(new Point(position.X + 1, position.Y)))
            {
                queue.Enqueue((new Point(position.X + 1, position.Y), item.Steps + 1), item.Steps + 1);

                visited.Add(new Point(position.X + 1, position.Y));
            }

            if (position.Y < _height - 1 && ! _maze[position.X, position.Y + 1] && ! visited.Contains(new Point(position.X, position.Y + 1)))
            {
                queue.Enqueue((new Point(position.X, position.Y + 1), item.Steps + 1), item.Steps + 1);

                visited.Add(new Point(position.X, position.Y + 1));
            }

            if (position.X > 0 && ! _maze[position.X - 1, position.Y] && ! visited.Contains(new Point(position.X - 1, position.Y)))
            {
                queue.Enqueue((new Point(position.X - 1, position.Y), item.Steps + 1), item.Steps + 1);

                visited.Add(new Point(position.X - 1, position.Y));
            }
        }

        return steps;
    }

    private void BuildMaze(int destinationX, int destinationY)
    {
        _width = destinationX + 1;

        _height = destinationY + 1;

        _maze = new bool[_width, _height];

        var designerNumber = int.Parse(Input[0]);

        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
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
}