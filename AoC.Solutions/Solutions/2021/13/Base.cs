using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2021._13;

public abstract class Base : Solution
{
    protected readonly List<Point> Dots = new();

    private readonly List<(string, int)> _folds = new();

    protected void ParseInput()
    {
        foreach (var line in Input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            if (line.StartsWith("fold"))
            {
                var fold = line.Split('=');

                _folds.Add((fold[0].Substring(fold[0].Length - 1), int.Parse(fold[1])));

                continue;
            }

            var coords = line.Split(',', StringSplitOptions.TrimEntries);

            Dots.Add(new Point(int.Parse(coords[0]), int.Parse(coords[1])));
        }
    }

    protected void DoFolds(bool firstOnly = false)
    {
        foreach (var fold in _folds)
        {
            if (fold.Item1 == "x")
            {
                var foldedX = Dots.Where(d => d.X > fold.Item2).ToList();

                foreach (var point in foldedX)
                {
                    Dots.Remove(point);

                    var newX = fold.Item2 - (point.X - fold.Item2);

                    if (! Dots.Any(d => d.X == newX && d.Y == point.Y))
                    {
                        Dots.Add(new Point(newX, point.Y));
                    }
                }
            }
            else
            {
                var foldedY = Dots.Where(d => d.Y > fold.Item2).ToList();

                foreach (var point in foldedY)
                {
                    Dots.Remove(point);

                    var newY = fold.Item2 - (point.Y - fold.Item2);

                    if (! Dots.Any(d => d.X == point.X && d.Y == newY))
                    {
                        Dots.Add(new Point(point.X, newY));
                    }
                }
            }

            if (firstOnly)
            {
                break;
            }
        }
    }
}