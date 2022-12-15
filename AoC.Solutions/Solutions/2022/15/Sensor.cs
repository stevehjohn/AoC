using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2022._15;

public class Sensor
{
    public Point Position { get; }

    public Point ClosestBeacon { get; }

    public int ManhattanRange => Math.Abs(Position.X - ClosestBeacon.X) + Math.Abs(Position.Y - ClosestBeacon.Y);

    public Sensor(Point position, Point closestBeacon)
    {
        Position = position;

        ClosestBeacon = closestBeacon;
    }
}