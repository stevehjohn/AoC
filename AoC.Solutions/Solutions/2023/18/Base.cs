using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._18;

public abstract class Base : Solution
{
    public override string Description => "Lavaduct lagoon";

    protected readonly List<(long X, long Y)> Points = new ();

    protected long Length = 1;
    
    protected long GetArea()
    {
        var shiftedPoints = Points.Skip(1).Append(Points[0]);

        var zipped = Points.Zip(shiftedPoints);

        var laces = zipped.Select(l => l.First.X * l.Second.Y - l.First.Y * l.Second.X);

        var area = Math.Abs(laces.Sum()) / 2;
        
        area += Length / 2 + 1;

        return area;
    }
}