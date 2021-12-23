using AoC.Solutions.Common;
using AoC.Solutions.Exceptions;
using AoC.Solutions.Extensions;

namespace AoC.Solutions.Solutions._2021._19;

public class Transform
{
    private readonly PointCloud _origin;

    private readonly PointCloud _target;

    private readonly Pair _pair;

    private TransformParameters _parameters;

    public Transform(PointCloud origin, PointCloud target, Pair pair)
    {
        _origin = origin;

        _target = target;

        _pair = pair;
    }

    public Point TransformPoint(Point point)
    {
        if (_parameters == null)
        {
            CalculateParameters();
        }

        var origin = new PointDecimal(_pair.Beacon1);

        var target = RotatePoint(new PointDecimal(_pair.Beacon2), _parameters);

        var x = RotatePoint(new PointDecimal(point), _parameters);

        return new Point
               {
                   X = (int) (x.X + (origin.X - target.X)),
                   Y = (int) (x.Y + (origin.Y - target.Y)),
                   Z = (int) (x.Z + (origin.Z - target.Z))
               };
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
                _parameters = combination;

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
        foreach (var originPoint in _origin.Points)
        {
            var transformed = RotatePoint(originPoint, parameters);

            var match = _target.Points.SingleOrDefault(p => p.Equals(transformed));

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