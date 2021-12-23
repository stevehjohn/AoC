using AoC.Solutions.Common;

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
        _origin.CentreAtZero();

        _target.CentreAtZero();
    }
}