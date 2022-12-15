using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2022._15;

public class Sensor
{
    public Point Position { get; }

    public int ManhattanRange { get; }

    public Sensor(Point position, Point closestBeacon)
    {
        Position = position;

        ManhattanRange = Math.Abs(Position.X - closestBeacon.X) + Math.Abs(Position.Y - closestBeacon.Y);
    }
}