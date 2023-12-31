using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._18;

public abstract class Base : Solution
{
    public override string Description => "Boiling boulders";

    private const int GridSize = 27;

    private readonly int[,,] _grid = new int[GridSize, GridSize, GridSize];

    protected int BuildGridAndReturnExposedFaceCount()
    {
        var sides = 0;

        foreach (var line in Input)
        {
            var point = Point.Parse(line);

            point.X += 2;
            point.Y += 2;
            point.Z += 2;

            _grid[point.X, point.Y, point.Z] = 1;

            var adjacent = CountAdjacent(point);

            sides -= adjacent;

            sides += 6 - adjacent;
        }

        return sides;
    }

    protected int CountSurfaceArea()
    {
        var count = 0;

        var queue = new Queue<Point>();

        queue.Enqueue(new Point(GridSize - 2, GridSize - 2, GridSize - 2));

        while (queue.Count > 0)
        {
            var point = queue.Dequeue();

            if (_grid[point.X, point.Y, point.Z] == 2)
            {
                continue;
            }

            _grid[point.X, point.Y, point.Z] = 2;

            count += CountAdjacent(point);

            var pointsToVisit = GetAdjacentPointsToVisit(point);

            pointsToVisit.ForEach(queue.Enqueue);
        }
        return count;
    }

    private List<Point> GetAdjacentPointsToVisit(Point point)
    {
        var points = new List<Point>();

        if (point.X > 1 && _grid[point.X - 1, point.Y, point.Z] == 0)
        {
            points.Add(new Point(point.X - 1, point.Y, point.Z));
        }

        if (point.X < GridSize - 2 && _grid[point.X + 1, point.Y, point.Z] == 0)
        {
            points.Add(new Point(point.X + 1, point.Y, point.Z));
        }

        if (point.Y > 1 && _grid[point.X, point.Y - 1, point.Z] == 0)
        {
            points.Add(new Point(point.X, point.Y - 1, point.Z));
        }

        if (point.Y < GridSize - 2 && _grid[point.X, point.Y + 1, point.Z] == 0)
        {
            points.Add(new Point(point.X, point.Y + 1, point.Z));
        }

        if (point.Z > 1 && _grid[point.X, point.Y, point.Z - 1] == 0)
        {
            points.Add(new Point(point.X, point.Y, point.Z - 1));
        }

        if (point.Z < GridSize - 2 && _grid[point.X, point.Y, point.Z + 1] == 0)
        {
            points.Add(new Point(point.X, point.Y, point.Z + 1));
        }

        return points;
    }

    private int CountAdjacent(Point point)
    {
        var count = 0;

        if (_grid[point.X - 1, point.Y, point.Z] == 1)
        {
            count++;
        }

        if (_grid[point.X + 1, point.Y, point.Z] == 1)
        {
            count++;
        }

        if (_grid[point.X, point.Y - 1, point.Z] == 1)
        {
            count++;
        }

        if (_grid[point.X, point.Y + 1, point.Z] == 1)
        {
            count++;
        }

        if (_grid[point.X, point.Y, point.Z - 1] == 1)
        {
            count++;
        }

        if (_grid[point.X, point.Y, point.Z + 1] == 1)
        {
            count++;
        }

        return count;
    }
}