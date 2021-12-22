using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2021._19;

public class Transform
{
    private readonly Axis[] _mappings = new Axis[3];

    private readonly int[] _deltas = new int[3];

    public Point TransformPoint(Point origin)
    {
        var point = new Point(origin)
                    {
                        X = (_mappings[0] == Axis.X
                                 ? origin.X
                                 : _mappings[0] == Axis.Y
                                     ? origin.Y
                                     : origin.Z)
                            + _deltas[(int)_mappings[0]],

                        Y = (_mappings[1] == Axis.X
                                 ? origin.X
                                 : _mappings[1] == Axis.Y
                                     ? origin.Y
                                     : origin.Z)
                            + _deltas[(int)_mappings[1]],

                        Z = (_mappings[2] == Axis.X
                                 ? origin.X
                                 : _mappings[2] == Axis.Y
                                     ? origin.Y
                                     : origin.Z)
                            + _deltas[(int)_mappings[2]]
                    };

        return point;
    }

    // TODO: Could there be fewer calls to TryAxisMapping?
    public void CalculateTransform(Point origin, Point target, int xDelta, int yDelta, int zDelta)
    {
        _deltas[0] = xDelta;

        _deltas[1] = yDelta;
        
        _deltas[2] = zDelta;

        // Getting there... there's an issue with the origin's +/- and the delta's +/-
    }

    private void TryAxisMapping(int left, int right, int delta, Axis origin, Axis target)
    {
        var match = TryMatch(left, right, delta);

        if (match != null)
        {
            _mappings[(int) target] = origin;
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