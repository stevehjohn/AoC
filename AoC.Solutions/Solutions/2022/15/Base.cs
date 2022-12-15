using AoC.Solutions.Common;
using AoC.Solutions.Exceptions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._15;

public abstract class Base : Solution
{
    public override string Description => "Beacon exclusion zone";

    private readonly List<Sensor> _sensors = new();

    private int _minX = int.MaxValue;

    private int _maxX = int.MinValue;

    protected void ParseInput()
    {
        foreach (var line in Input)
        {
            var split = line.Split(':', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            var positionString = split[0][10..].Replace("x=", string.Empty).Replace("y=", string.Empty);

            var position = Point.Parse(positionString);

            var beaconString = split[1][21..].Replace("x=", string.Empty).Replace("y=", string.Empty);

            var beacon = Point.Parse(beaconString);

            var sensor = new Sensor(position, beacon);

            if (sensor.Position.X - sensor.ManhattanRange < _minX)
            {
                _minX = sensor.Position.X - sensor.ManhattanRange;
            }

            if (sensor.Position.X + sensor.ManhattanRange > _maxX)
            {
                _maxX = sensor.Position.X + sensor.ManhattanRange;
            }

            _sensors.Add(sensor);
        }
    }

    protected int GetDeadZones(int y)
    {
        var covered = 0;

        var dead = 0;
        
        for (var x = _minX; x < _maxX; x++)
        {
            var isCovered = false;

            foreach (var sensor in _sensors)
            {
                var manhattanDistance = Math.Abs(x - sensor.Position.X) + Math.Abs(y - sensor.Position.Y);

                if (manhattanDistance <= sensor.ManhattanRange)
                {
                    isCovered = true;

                    break;
                }
            }

            if (isCovered)
            {
                covered++;

                if (_sensors.Exists(s => s.ClosestBeacon.Equals(new Point(x, y))))
                {
                    dead++;
                }
            }
        }

        return covered - dead;
    }

    protected Point GetDeadZone(int range)
    {
        for (var y = 0; y <= range; y++)
        {
            var covered = new List<(int L, int R)>();

            foreach (var sensor in _sensors)
            {
                var dY = Math.Abs(sensor.Position.Y - y);

                if (dY > sensor.ManhattanRange)
                {
                    continue;
                }

                var lineRange = sensor.ManhattanRange - dY;

                var l = sensor.Position.X - lineRange;

                var r = sensor.Position.X + lineRange;

                covered.Add((l, r));
            }

            var range1 = covered[0];

            covered.RemoveAt(0);

            var changed = true;

            while (changed)
            {
                changed = false;

                var i = 0;

                while (i < covered.Count)
                {
                    var item = covered[i];

                    if ((item.L >= range1.L - 1 && item.L <= range1.R + 1)
                        || (item.R >= range1.L - 1 && item.R <= range1.R + 1)
                        || (range1.L >= item.L - 1 && range1.L <= item.R + 1) 
                        || (range1.R >= item.L - 1 && range1.R <= item.R + 1))
                    {
                        range1.L = Math.Min(range1.L, item.L);
                        range1.R = Math.Max(range1.R, item.R);

                        covered.RemoveAt(i);

                        changed = true;

                        continue;
                    }

                    i++;
                }
            }

            if (covered.Count == 0)
            {
                continue;
            }

            var range2 = covered[0];

            covered.RemoveAt(0);

            changed = true;

            while (changed)
            {
                changed = false;

                var i = 0;

                while (i < covered.Count)
                {
                    var item = covered[i];

                    if ((item.L >= range2.L - 1 && item.L <= range2.R + 1)
                        || (item.R >= range2.L - 1 && item.R <= range2.R + 1)
                        || (range2.L >= item.L - 1 && range2.L <= item.R + 1) 
                        || (range2.R >= item.L - 1 && range2.R <= item.R + 1))
                    {
                        range2.L = Math.Min(range2.L, item.L);
                        range2.R = Math.Max(range2.R, item.R);

                        covered.RemoveAt(i);

                        changed = true;

                        continue;
                    }

                    i++;
                }
            }

            var lBound = Math.Min(range1.R, range2.R);

            return new Point(lBound + 1, y);
        }

        throw new PuzzleException("Solution not found");
    }
}