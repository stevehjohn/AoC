namespace AoC.Solutions.Solutions._2021._19;

public class PointDouble
{
    public double X { get; set; }

    public double Y { get; set; }

    public double Z { get; set; }

    public PointDouble(double x, double y, double z = 0)
    {
        X = x;

        Y = y;

        Z = z;
    }

    public override string ToString()
    {
        return $"{X,4},{Y,4},{Z,4}";
    }
}