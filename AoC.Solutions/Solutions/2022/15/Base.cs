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

            if (position.X < _minX)
            {
                _minX = position.X;
            }

            if (position.X > _maxX)
            {
                _maxX = position.X;
            }

            var beaconString = split[1][21..].Replace("x=", string.Empty).Replace("y=", string.Empty);

            var beacon = Point.Parse(beaconString);

            if (beacon.X < _minX)
            {
                _minX = beacon.X;
            }

            if (beacon.X > _maxX)
            {
                _maxX = beacon.X;
            }

            var sensor = new Sensor(position, beacon);

            _sensors.Add(sensor);
        }
    }

    protected int GetDeadZones(int y)
    {
        var dead = 0;

        for (var x = _minX; x < _maxX; x++)
        {
            var beaconFound = false;

            var position = new Point(x, y);

            foreach (var sensor in _sensors)
            {
                var manhattanDistance = Math.Abs(x - sensor.Position.X) + Math.Abs(y - sensor.Position.Y);

                if (manhattanDistance <= sensor.ManhattanRange)
                {
                    if (_sensors.Exists(s => s.ClosestBeacon.Equals(position)))
                    {
                        beaconFound = true;

                        break;
                    }
                }
            }

            //Console.WriteLine($"{x}: {beaconFound}");

            if (! beaconFound)
            {
                dead++;
            }
            else
            {
                Console.WriteLine($"{x}");
            }
        }

        return dead;
    }
}