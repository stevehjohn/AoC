using AoC.Solutions.Common;
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
}