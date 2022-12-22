using AoC.Solutions.Common;
using AoC.Solutions.Exceptions;
using AoC.Solutions.Infrastructure;
using System.Runtime.CompilerServices;

namespace AoC.Solutions.Solutions._2022._15;

public abstract class Base : Solution
{
    public override string Description => "Beacon exclusion zone";

    private List<Sensor> _sensors = new();

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
        var ranges = new List<(int L, int R)>();

        foreach (var sensor in _sensors)
        {
            var dY = Math.Abs(sensor.Position.Y - y);

            if (dY >= sensor.ManhattanRange)
            {
                continue;
            }

            var lineRange = sensor.ManhattanRange - dY;

            var l = sensor.Position.X - lineRange;

            var r = sensor.Position.X + lineRange;

            ranges.Add((l, r));
        }

        var range = Collapse(ranges);

        return -range.L + range.R;
    }

    protected Point GetDeadZone()
    {
        var (start, end) = GetRangeToScanAndCullSensors();

        var covered = new List<(int L, int R)>();

        for (var y = start; y <= end; y++)
        {
            covered.Clear();

            foreach (var sensor in _sensors)
            {
                var dY = Math.Abs(sensor.Position.Y - y);

                if (dY > sensor.ManhattanRange)
                {
                    continue;
                }

                var lineRange = sensor.ManhattanRange - dY;

                covered.Add((sensor.Position.X - lineRange, sensor.Position.X + lineRange));
            }

            var range1 = Collapse(covered);

            if (covered.Count == 0)
            {
                continue;
            }

            var range2 = Collapse(covered);

            // Maybe some data sets would require "range1.L - range2.R == 2"? WOMMAD (Works On My Machine And Data) anyhoo.
            if (range2.L - range1.R == 2)
            {
                return new Point(range1.R + 1, y);
            }
        }

        throw new PuzzleException("Solution not found");
    }

    private (int Start, int End) GetRangeToScanAndCullSensors()
    {
        var start = int.MaxValue;

        var end = int.MinValue;

        var culled = new List<Sensor>();

        for (var o = 0; o < _sensors.Count; o++)
        {
            var outer = _sensors[o];

            for (var i = o + 1; i < _sensors.Count; i++)
            {
                var inner = _sensors[i];

                var targetDelta = outer.ManhattanRange + inner.ManhattanRange + 2;

                var delta = Math.Abs(outer.Position.X - inner.Position.X) + Math.Abs(outer.Position.Y - inner.Position.Y);

                if (delta == targetDelta)
                {
                    var min = Math.Min(outer.Position.Y, inner.Position.Y);

                    if (min < start)
                    {
                        start = min;
                    }

                    var max = Math.Max(outer.Position.Y, inner.Position.Y);

                    if (max > end)
                    {
                        end = max;
                    }

                    culled.Add(inner);

                    culled.Add(outer);
                }
            }
        }

        _sensors = culled;

        return (start, end);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static (int L, int R) Collapse(List<(int L, int R)> ranges)
    {
        var range = ranges[0];

        ranges.RemoveAt(0);

        var changed = true;

        while (changed)
        {
            changed = false;

            var i = 0;

            while (i < ranges.Count)
            {
                var item = ranges[i];

                if ((item.L >= range.L - 1 && item.L <= range.R + 1)
                    || (item.R >= range.L - 1 && item.R <= range.R + 1))
                {
                    range.L = Math.Min(range.L, item.L);
                    range.R = Math.Max(range.R, item.R);

                    ranges.RemoveAt(i);

                    changed = true;

                    continue;
                }

                i++;
            }
        }

        return range;
    }
}