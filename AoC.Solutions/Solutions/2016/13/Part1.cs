using AoC.Solutions.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._13;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        Width = 32;

        Height = 40;

        BuildMaze();

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

            if (position.Y > 0 && ! Maze[position.X, position.Y - 1] && ! visited.Contains(new Point(position.X, position.Y - 1)))
            {
                queue.Enqueue((new Point(position.X, position.Y - 1), item.Steps + 1), item.Steps + 1);

                visited.Add(new Point(position.X, position.Y - 1));
            }

            if (position.X < Width - 1 && ! Maze[position.X + 1, position.Y] && ! visited.Contains(new Point(position.X + 1, position.Y)))
            {
                queue.Enqueue((new Point(position.X + 1, position.Y), item.Steps + 1), item.Steps + 1);

                visited.Add(new Point(position.X + 1, position.Y));
            }

            if (position.Y < Height - 1 && ! Maze[position.X, position.Y + 1] && ! visited.Contains(new Point(position.X, position.Y + 1)))
            {
                queue.Enqueue((new Point(position.X, position.Y + 1), item.Steps + 1), item.Steps + 1);

                visited.Add(new Point(position.X, position.Y + 1));
            }

            if (position.X > 0 && ! Maze[position.X - 1, position.Y] && ! visited.Contains(new Point(position.X - 1, position.Y)))
            {
                queue.Enqueue((new Point(position.X - 1, position.Y), item.Steps + 1), item.Steps + 1);

                visited.Add(new Point(position.X - 1, position.Y));
            }
        }

        return steps;
    }
}