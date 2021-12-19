using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2021._19;

public class Scanner
{
    public int Id { get; }

    public List<Point> Beacons { get; }
    
    public List<Point> NormalisedBeacons 
    {
        get
        {
            if (_normalisedBeacons == null)
            {
                CalculateNormalisedCoordinates();
            }

            return _normalisedBeacons;
        }
    }

    private List<Point> _normalisedBeacons;

    public Scanner(int id)
    {
        Id = id;

        Beacons = new List<Point>();
    }

    public void AddBeacon(int x, int y, int z)
    {
        Beacons.Add(new Point(x, y, z));
    }

    public void CalculateNormalisedCoordinates()
    {
        _normalisedBeacons = new List<Point>();

        var xBase = -Beacons.Min(b => b.X);
        
        var yBase = -Beacons.Min(b => b.Y);

        var zBase = -Beacons.Min(b => b.Z);

        foreach (var beacon in Beacons)
        {
            _normalisedBeacons.Add(new Point(xBase + beacon.X, yBase + beacon.Y, zBase + beacon.Z));
        }
    }
}