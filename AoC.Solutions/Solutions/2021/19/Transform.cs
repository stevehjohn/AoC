using AoC.Solutions.Common;
using AoC.Solutions.Exceptions;
using AoC.Solutions.Extensions;

namespace AoC.Solutions.Solutions._2021._19;

public class Transform
{
    private readonly PointCloud _origin;

    private readonly PointCloud _target;

    private readonly Pair _pair;

    private TransformParameters Parameters { get; set; }

    public Transform(PointCloud origin, PointCloud target, Pair pair)
    {
        _origin = origin;

        _target = target;

        _pair = pair;
    }

    public Point TransformPoint(Scanner origin)
    {
        if (Parameters == null)
        {
            CalculateParameters();
        }

        var beacon1 = new PointDecimal(_pair.Beacon1.X, _pair.Beacon1.Y, _pair.Beacon1.Z);

        var beacon2 = new PointDecimal(_pair.Beacon2.X, _pair.Beacon2.Y, _pair.Beacon2.Z);

        beacon2 = RotatePoint(beacon2, Parameters);

        var delta = new PointDecimal(beacon1.X - beacon2.X, beacon1.Y - beacon2.Y, beacon1.Z - beacon2.Z);

        var node = origin;

        while (node != null && node.Transform != null)
        {
            delta = RotatePoint(delta, node.Transform.Parameters);

            node = node.Origin;
        }

        var point = origin.Position;

        var result = new Point((int) (point.X + delta.X), (int) (point.Y + delta.Y), (int) (point.Z + delta.Z));

        return result;
    }

    private void CalculateParameters()
    {
        CentreClouds();

        FindRotation();
    }

    private void CentreClouds()
    {
        _origin.CentreAtZero();

        _target.CentreAtZero();
    }

    private void FindRotation()
    {
        var parameterCombinations = GetParameterCombinations();

        foreach (var combination in parameterCombinations)
        {
            if (CheckCloudsMatch(combination))
            {
                Parameters = combination;

                return;
            }
        }

        throw new PuzzleException("No translation found.");
    }

    private static List<TransformParameters> GetParameterCombinations()
    {
        var permutations = new List<TransformParameters>();

        var axisPermutations = new[] { Axis.X, Axis.Y, Axis.Z }.GetPermutations();

        foreach (var axisPermutation in axisPermutations)
        {
            permutations.Add(new TransformParameters(axisPermutation, new[] { Sign.Positive, Sign.Positive, Sign.Positive }));

            permutations.Add(new TransformParameters(axisPermutation, new[] { Sign.Positive, Sign.Positive, Sign.Negative }));

            permutations.Add(new TransformParameters(axisPermutation, new[] { Sign.Positive, Sign.Negative, Sign.Positive }));

            permutations.Add(new TransformParameters(axisPermutation, new[] { Sign.Positive, Sign.Negative, Sign.Negative }));

            permutations.Add(new TransformParameters(axisPermutation, new[] { Sign.Negative, Sign.Positive, Sign.Positive }));

            permutations.Add(new TransformParameters(axisPermutation, new[] { Sign.Negative, Sign.Positive, Sign.Negative }));

            permutations.Add(new TransformParameters(axisPermutation, new[] { Sign.Negative, Sign.Negative, Sign.Positive }));

            permutations.Add(new TransformParameters(axisPermutation, new[] { Sign.Negative, Sign.Negative, Sign.Negative }));
        }

        return permutations;
    }

    private bool CheckCloudsMatch(TransformParameters parameters)
    {
        foreach (var targetPoint in _target.Points)
        {
            var transformed = RotatePoint(targetPoint, parameters);

            var match = _origin.Points.SingleOrDefault(p => p.Equals(transformed));

            if (match == null)
            {
                return false;
            }
        }

        return true;
    }

    private static PointDecimal RotatePoint(PointDecimal point, TransformParameters parameters)
    {
        var transformed = new PointDecimal
                          {
                              X = (int) parameters.Flips[(int) Axis.X] * (parameters.Mappings[(int) Axis.X] == Axis.X
                                                                              ? point.X
                                                                              : parameters.Mappings[(int) Axis.X] == Axis.Y
                                                                                  ? point.Y
                                                                                  : point.Z),

                              Y = (int) parameters.Flips[(int) Axis.Y] * (parameters.Mappings[(int) Axis.Y] == Axis.X
                                                                              ? point.X
                                                                              : parameters.Mappings[(int) Axis.Y] == Axis.Y
                                                                                  ? point.Y
                                                                                  : point.Z),

                              Z = (int) parameters.Flips[(int) Axis.Z] * (parameters.Mappings[(int) Axis.Z] == Axis.X
                                                                              ? point.X
                                                                              : parameters.Mappings[(int) Axis.Z] == Axis.Y
                                                                                  ? point.Y
                                                                                  : point.Z)
                          };

        return transformed;
    }
}