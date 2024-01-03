using AoC.Solutions.Common;
using AoC.Solutions.Extensions;

namespace AoC.Solutions.Solutions._2021._05;

public class Line
{
    public Point Start { get; set; }

    public Point End { get; set; }

    public bool IsAxial => Start.X == End.X || Start.Y == End.Y;

    public static Line Parse(string data)
    {
        var coords = data.Replace("->", ",").Split(",", StringSplitOptions.TrimEntries).Select(int.Parse).ToList();

        var line = new Line
                   {
                       Start = new Point(coords[0], coords[1]),
                       End = new Point(coords[2], coords[3])
                   };

        return line;
    }

    public List<Point> GetPoints()
    {
        var points = new List<Point>();

        var point = new Point(Start);

        points.Add(new Point(point));

        while (point.X != End.X || point.Y != End.Y)
        {
            point.X = point.X.Converge(End.X);

            point.Y = point.Y.Converge(End.Y);

            points.Add(new Point(point));
        }

        return points;
    }
}