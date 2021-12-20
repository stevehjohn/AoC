using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2021._19;

public class DisorientedPoint : Point
{
    public List<Point> Orientations 
    {
        get
        {
            if (_orientations == null)
            {
                CalculateOrientations();
            }

            return _orientations;
        }
    }

    private List<Point> _orientations;

    public DisorientedPoint(int x, int y, int z) : base(x, y, z)
    {
    }

    public bool Matches(DisorientedPoint other)
    {
        return Orientations.FirstOrDefault(o => other.Orientations.Any(o.Equals)) != null;
    }

    private void CalculateOrientations()
    {
        _orientations = new List<Point>
                        {
                            new(-X, Y, Z),
                            new(X, Y, Z),
                            new(-X, -Y, Z),
                            new(X, -Y, Z),
                            new(-X, Y, -Z),
                            new(X, Y, -Z),
                            new(-X, -Y, -Z),
                            new(X, -Y, -Z),

                            new(-Y, Z, X),
                            new(Y, Z, X),
                            new(-Y, -Z, X),
                            new(Y, -Z, X),
                            new(-Y, Z, -X),
                            new(Y, Z, -X),
                            new(-Y, -Z, -X),
                            new(Y, -Z, -X),

                            new(-Z, X, Y),
                            new(Z, X, Y),
                            new(-Z, -X, Y),
                            new(Z, -X, Y),
                            new(-Z, X, -Y),
                            new(Z, X, -Y),
                            new(-Z, -X, -Y),
                            new(Z, -X, -Y)
                        };
    }
}