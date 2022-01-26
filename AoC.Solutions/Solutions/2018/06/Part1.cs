using AoC.Solutions.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._06;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly List<Point> _points = new();

    private int _width;

    private int _height;

    //private int[,] _map;

    private readonly Dictionary<Point, int> _counts = new();

    private readonly HashSet<Point> _infinitePoints = new();

    public override string GetAnswer()
    {
        ParseInput();

        MapClosestPoints();

        //Dump();

        var nonInfinite = _counts.Where(p => ! _infinitePoints.Contains(p.Key));

        return nonInfinite.Max(c => c.Value).ToString();
    }

    //private void Dump()
    //{
    //    //for (var y = 0; y < _height; y++)
    //    //{
    //    //    for (var x = 0; x < _width; x++)
    //    //    {
    //    //        Console.Write((char) ('a' + _map[x, y]));
    //    //    }

    //    //    Console.WriteLine();
    //    //}

    //    foreach (var count in _counts)
    //    {
    //        Console.WriteLine($"{count.Key}: {count.Value}");
    //    }
    //}

    private void MapClosestPoints()
    {
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                var position = new Point(x, y);

                var distances = _points.Select((p, i) => (Distance: ManhattanDistance(p, position), Index: i, Point: p)).OrderBy(d => d.Distance).ToList();

                if (distances[0].Distance != distances[1].Distance)
                {
                    if (_counts.ContainsKey(distances[0].Point))
                    {
                        _counts[distances[0].Point]++;
                    }
                    else
                    {
                        _counts.Add(distances[0].Point, 1);
                    }

                    if (position.X == 0 || position.X == _width - 1 || position.Y == 0 || position.Y == _height - 1)
                    {
                        _infinitePoints.Add(distances[0].Point);
                    }

                    //_map[x, y] = distances[0].Index;
                }
                else
                {
                    //_map[x, y] = -51;
                }
            }
        }
    }

    private static int ManhattanDistance(Point left, Point right)
    {
        return Math.Abs(left.X - right.X) + Math.Abs(left.Y - right.Y);
    }

    private void ParseInput()
    {
        var points = new List<Point>();

        foreach (var line in Input)
        {
            points.Add(Point.Parse(line));
        }

        var xMin = points.Min(p => p.X);

        var yMin = points.Min(p => p.Y);

        foreach (var point in points)
        {
            _points.Add(new Point(point.X - xMin, point.Y - yMin));
        }

        _width = _points.Max(p => p.X) + 1;

        _height = _points.Max(p => p.Y) + 1;

        //_map = new int[_width, _height];
    }
}