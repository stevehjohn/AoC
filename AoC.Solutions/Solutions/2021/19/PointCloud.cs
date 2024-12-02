using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2021._19;

public class PointCloud
{
    public List<PointDecimal> Points { get; }

    public PointCloud(List<Point> points)
    {
        Points = points.Select(p => new PointDecimal(p)).ToList();
    }

    public void CentreAtZero()
    {
        var relativeCentre = new PointDecimal((Points.Min(p => p.X) + Points.Max(p => p.X)) / 2,
                                              (Points.Min(p => p.Y) + Points.Max(p => p.Y)) / 2,
                                              (Points.Min(p => p.Z) + Points.Max(p => p.Z)) / 2);

        foreach (var point in Points)
        {
            point.X -= relativeCentre.X;

            point.Y -= relativeCentre.Y;
            
            point.Z -= relativeCentre.Z;
        }
    }
}