using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2021._19;

public class Transform
{
    private readonly List<TransformParameters> _transformParameters = new();

    public Point TransformPoint(Point origin)
    {
        var parameters = _transformParameters[0];

        var x = ((parameters.Signs[0] == Sign.Negative ? -1 : 1)
                 * (parameters.Mappings[0] == Axis.X
                        ? origin.X
                        : parameters.Mappings[0] == Axis.Y
                            ? origin.Y
                            : origin.Z)
                 + parameters.Deltas[0]) * (parameters.FlipResult[0] == Sign.Negative ? -1 : 1);

        var y = ((parameters.Signs[1] == Sign.Negative ? -1 : 1)
                 * (parameters.Mappings[1] == Axis.X
                        ? origin.X
                        : parameters.Mappings[1] == Axis.Y
                            ? origin.Y
                            : origin.Z)
                 + parameters.Deltas[1]) * (parameters.FlipResult[1] == Sign.Negative ? -1 : 1);

        var z = ((parameters.Signs[2] == Sign.Negative ? -1 : 1)
                 * (parameters.Mappings[2] == Axis.X
                        ? origin.X
                        : parameters.Mappings[2] == Axis.Y
                            ? origin.Y
                            : origin.Z)
                 + parameters.Deltas[2]) * (parameters.FlipResult[2] == Sign.Negative ? -1 : 1);

        var point = new Point
                    {
                        X = x,
                        Y = y,
                        Z = z
        };

        return point;
    }

    public void CalculateTransform(Point origin, Point target, int xDelta, int yDelta, int zDelta)
    {
        // So, looks like TryAxisMapping could match 2 conditions - which is correct?
        TryAxisMapping(origin.X, target.X, xDelta, Axis.X, Axis.X);
        TryAxisMapping(origin.X, target.Y, xDelta, Axis.X, Axis.Y);
        TryAxisMapping(origin.X, target.Z, xDelta, Axis.X, Axis.Z);

        TryAxisMapping(origin.Y, target.X, yDelta, Axis.Y, Axis.X);
        TryAxisMapping(origin.Y, target.Y, yDelta, Axis.Y, Axis.Y);
        TryAxisMapping(origin.Y, target.Z, yDelta, Axis.Y, Axis.Z);

        TryAxisMapping(origin.Z, target.X, zDelta, Axis.Z, Axis.X);
        TryAxisMapping(origin.Z, target.Y, zDelta, Axis.Z, Axis.Y);
        TryAxisMapping(origin.Z, target.Z, zDelta, Axis.Z, Axis.Z);

        // Looks like it's just luck which order the deltas end up in...

        //foreach (var parameter in _transformParameters)
        //{
        //    Console.WriteLine($"{parameter.Deltas[0]}, {parameter.Deltas[1]}, {parameter.Deltas[2]}");
        //}
    }

    private void TryAxisMapping(int left, int right, int delta, Axis origin, Axis target)
    {
        if (left + delta == right)
        {
            AddMappingParameter(target, origin, delta, Sign.Positive, Sign.Positive);
        }

        //if (left + delta == -right)
        //{
        //    AddMappingParameter(target, origin, delta, Sign.Positive, Sign.Negative);
        //}

        if (left - delta == right)
        {
            AddMappingParameter(target, origin, -delta, Sign.Positive, Sign.Positive);
        }

        //if (left - delta == -right)
        //{
        //    AddMappingParameter(target, origin, -delta, Sign.Positive, Sign.Negative);
        //}

        if (-left + delta == right)
        {
            AddMappingParameter(target, origin, delta, Sign.Negative, Sign.Positive);
        }

        //if (-left + delta == -right)
        //{
        //    AddMappingParameter(target, origin, delta, Sign.Negative, Sign.Negative);
        //}

        if (-left - delta == right)
        {
            AddMappingParameter(target, origin, -delta, Sign.Negative, Sign.Positive);
        }

        //if (-left - delta == -right)
        //{
        //    AddMappingParameter(target, origin, -delta, Sign.Negative, Sign.Negative);
        //}
    }

    private void AddMappingParameter(Axis target, Axis origin, int delta, Sign sign, Sign flipResult)
    {
        var parameters = _transformParameters.FirstOrDefault(); //(p => p.Mappings[(int) target] == Axis.Unknown);

        if (parameters == null)
        {
            parameters = new TransformParameters();

            _transformParameters.Add(parameters);
        }

        parameters.Mappings[(int) target] = origin;

        parameters.Deltas[(int) target] = delta;

        parameters.Signs[(int) target] = sign;

        parameters.FlipResult[(int) target] = flipResult;
    }
}