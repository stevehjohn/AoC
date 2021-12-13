using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._13;

[UsedImplicitly]
public class Part1 : Solution
{
    private readonly List<Point> _dots = new();

    private readonly List<(string, int)> _folds = new();

    public override string GetAnswer()
    {
        ParseInput();

        DoFolds();

        return _dots.Count.ToString();
    }

    private void ParseInput()
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

            _dots.Add(new Point(int.Parse(coords[0]), int.Parse(coords[1])));
        }
    }

    private void DoFolds()
    {
        foreach (var fold in _folds)
        {
            if (fold.Item1 == "x")
            {
                var folded = _dots.Where(d => d.X > fold.Item2).ToList();

                foreach (var point in folded)
                {
                    _dots.Remove(point);

                    var newX = fold.Item2 - (point.X - fold.Item2);

                    if (! _dots.Any(d => d.X == newX && d.Y == point.Y))
                    {
                        _dots.Add(new Point(newX, point.Y));
                    }
                }
            }

            break;
        }
    }
}