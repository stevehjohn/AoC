using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2021._19;

public class Transform
{
    private readonly Axis[] _mappings = new Axis[3];

    private readonly int[] _deltas = new int[3];

    public Point TransformPoint(Point origin)
    {
        var point = new Point(origin);

        point.X -= _deltas[(int) _mappings[0]];

        point.Y -= _deltas[(int) _mappings[1]];

        point.Z -= _deltas[(int) _mappings[2]];

        return point;
    }

    // TODO: Could there be fewer calls to TryAxisMapping?
    public void CalculateTransform(Point origin, Point target, int xDelta, int yDelta, int zDelta)
    {
        TryAxisMapping(origin.X, target.X, xDelta, Axis.X, Axis.X);

        TryAxisMapping(origin.X, target.X, yDelta, Axis.X, Axis.X);

        TryAxisMapping(origin.X, target.X, zDelta, Axis.X, Axis.X);

        TryAxisMapping(origin.X, target.Y, xDelta, Axis.X, Axis.Y);

        TryAxisMapping(origin.X, target.Y, yDelta, Axis.X, Axis.Y);

        TryAxisMapping(origin.X, target.Y, zDelta, Axis.X, Axis.Y);

        TryAxisMapping(origin.X, target.Z, xDelta, Axis.X, Axis.Z);

        TryAxisMapping(origin.X, target.Z, yDelta, Axis.X, Axis.Z);

        TryAxisMapping(origin.X, target.Z, zDelta, Axis.X, Axis.Z);


        TryAxisMapping(origin.Y, target.X, xDelta, Axis.Y, Axis.X);

        TryAxisMapping(origin.Y, target.X, yDelta, Axis.Y, Axis.X);

        TryAxisMapping(origin.Y, target.X, zDelta, Axis.Y, Axis.X);

        TryAxisMapping(origin.Y, target.Y, xDelta, Axis.Y, Axis.Y);

        TryAxisMapping(origin.Y, target.Y, yDelta, Axis.Y, Axis.Y);

        TryAxisMapping(origin.Y, target.Y, zDelta, Axis.Y, Axis.Y);

        TryAxisMapping(origin.Y, target.Z, xDelta, Axis.Y, Axis.Z);

        TryAxisMapping(origin.Y, target.Z, yDelta, Axis.Y, Axis.Z);

        TryAxisMapping(origin.Y, target.Z, zDelta, Axis.Y, Axis.Z);


        TryAxisMapping(origin.Z, target.X, xDelta, Axis.Z, Axis.X);

        TryAxisMapping(origin.Z, target.X, yDelta, Axis.Z, Axis.X);

        TryAxisMapping(origin.Z, target.X, zDelta, Axis.Z, Axis.X);

        TryAxisMapping(origin.Z, target.Y, xDelta, Axis.Z, Axis.Y);

        TryAxisMapping(origin.Z, target.Y, yDelta, Axis.Z, Axis.Y);

        TryAxisMapping(origin.Z, target.Y, zDelta, Axis.Z, Axis.Y);

        TryAxisMapping(origin.Z, target.Z, xDelta, Axis.Z, Axis.Z);

        TryAxisMapping(origin.Z, target.Z, yDelta, Axis.Z, Axis.Z);

        TryAxisMapping(origin.Z, target.Z, zDelta, Axis.Z, Axis.Z);
    }

    private void TryAxisMapping(int left, int right, int delta, Axis origin, Axis target)
    {
        var match = TryMatch(left, right, delta);

        if (match != null)
        {
            _mappings[(int) origin] = target;

            _deltas[(int) origin] = match.Value;
        }
    }

    private static int? TryMatch(int left, int right, int delta)
    {
        if (left + right == delta || left - right == delta)
        {
            return delta;
        }

        if (left - right == -delta || left + right == -delta)
        {
            return -delta;
        }

        return null;
    }
}