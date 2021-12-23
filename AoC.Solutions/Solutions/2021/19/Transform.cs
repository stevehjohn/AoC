using AoC.Solutions.Common;
using AoC.Solutions.Exceptions;
using AoC.Solutions.Extensions;

namespace AoC.Solutions.Solutions._2021._19;

public class Transform
{
    private readonly PointCloud _origin;

    private readonly PointCloud _target;

    private TransformParameters _parameters;

    public Transform(PointCloud origin, PointCloud target)
    {
        _origin = origin;

        _target = target;
    }

    public Point TransformPoint(Point point)
    {
        if (_parameters == null)
        {
            CalculateParameters();
        }

        return null;
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

    private List<TransformParameters> GetParameterCombinations()
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
        return false;
    }
}