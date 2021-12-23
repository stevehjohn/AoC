using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2021._19;

public class PointCloud
{
    private readonly List<PointDecimal> _points;

    public List<PointDecimal> Points => _points;

    public PointCloud(List<Point> points)
    {
        _points = points.Select(p => new PointDecimal(p)).ToList();
    }

    public void CentreAtZero()
    {
        var relativeCentre = new PointDecimal((_points.Min(p => p.X) + _points.Max(p => p.X)) / 2,
                                              (_points.Min(p => p.Y) + _points.Max(p => p.Y)) / 2,
                                              (_points.Min(p => p.Z) + _points.Max(p => p.Z)) / 2);

        foreach (var point in _points)
        {
            point.X -= relativeCentre.X;

            point.Y -= relativeCentre.Y;
            
            point.Z -= relativeCentre.Z;
        }
    }
}