using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2021._19;

public class Scanner
{
    public int Id { get; }

    public Point Position { get; set; }

    public List<Point> Beacons { get; }
    
    public List<PointDouble> NormalisedBeacons 
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

    private List<PointDouble> _normalisedBeacons;

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
        _normalisedBeacons = new List<PointDouble>();

        var xBase = -Beacons.Min(b => b.X);

        var yBase = -Beacons.Min(b => b.Y);

        var zBase = -Beacons.Min(b => b.Z);

        var neutralBase = -Math.Min(xBase, Math.Min(yBase, zBase));

        foreach (var beacon in Beacons)
        {
            var positiveSpacePoint = new PointDouble(neutralBase + beacon.X, neutralBase + beacon.Y, neutralBase + beacon.Z);

            _normalisedBeacons.Add(positiveSpacePoint);

            Console.WriteLine($"{beacon} -> {positiveSpacePoint}");
        }

        Console.WriteLine();

        //foreach (var beacon in NormalisedBeacons)
        //{
        //    beacon.X = 5000 - beacon.X;

        //    beacon.Y = 5000 - beacon.Y;

        //    beacon.Z = 5000 - beacon.Z;
        //}
    }

    public void TryGetPosition(Scanner origin)
    {
        var matchedPositions = new List<(Point, Point)>();

        for (var ob = 0; ob < origin.NormalisedBeacons.Count; ob++)
        {
            for (var b = 0; b < NormalisedBeacons.Count; b++)
            {
                if (MatchBeaconPosition(origin.NormalisedBeacons[ob], NormalisedBeacons[b]))
                {
                    matchedPositions.Add((origin.Beacons[ob], Beacons[b]));
                }

                //if (matchedPositions.Count == 2)
                //{
                //    Console.WriteLine("WIN");

                //    break;
                //}
            }

            //if (matchedPositions.Count == 2)
            //{
            //    Console.WriteLine("WIN");

            //    break;
            //}
        }

        Console.WriteLine(matchedPositions.Count);
    }

    private static bool MatchBeaconPosition(PointDouble left, PointDouble right)
    {
        // TODO: Centre here... 500 + each figure

        // ReSharper disable CompareOfFloatsByEqualityOperator - I know what I'm doing... maybe
        return 5000 + left.X == 5000 + -right.X && 5000 + left.Y == 5000 + right.Y && 5000 + left.Z == 5000 + right.Z
               || 5000 + left.X == 5000 + right.X && 5000 + left.Y == 5000 + right.Y && 5000 + left.Z == 5000 + right.Z
               || 5000 + left.X == 5000 + -right.X && 5000 + left.Y == 5000 + -right.Y && 5000 + left.Z == 5000 + right.Z
               || 5000 + left.X == 5000 + right.X && 5000 + left.Y == 5000 + -right.Y && 5000 + left.Z == 5000 + right.Z
               || 5000 + left.X == 5000 + -right.X && 5000 + left.Y == 5000 + right.Y && 5000 + left.Z == 5000 + -right.Z
               || 5000 + left.X == 5000 + right.X && 5000 + left.Y == 5000 + right.Y && 5000 + left.Z == 5000 + -right.Z
               || 5000 + left.X == 5000 + -right.X && 5000 + left.Y == 5000 + -right.Y && 5000 + left.Z == 5000 + -right.Z
               || 5000 + left.X == 5000 + right.X && 5000 + left.Y == 5000 + -right.Y && 5000 + left.Z == 5000 + -right.Z

               || 5000 + left.X == 5000 + -right.Y && 5000 + left.Y == 5000 + right.Z && 5000 + left.Z == 5000 + right.X
               || 5000 + left.X == 5000 + right.Y && 5000 + left.Y == 5000 + right.Z && 5000 + left.Z == 5000 + right.X
               || 5000 + left.X == 5000 + -right.Y && 5000 + left.Y == 5000 + -right.Z && 5000 + left.Z == 5000 + right.X
               || 5000 + left.X == 5000 + right.Y && 5000 + left.Y == 5000 + -right.Z && 5000 + left.Z == 5000 + right.X
               || 5000 + left.X == 5000 + -right.Y && 5000 + left.Y == 5000 + right.Z && 5000 + left.Z == 5000 + -right.X
               || 5000 + left.X == 5000 + right.Y && 5000 + left.Y == 5000 + right.Z && 5000 + left.Z == 5000 + -right.X
               || 5000 + left.X == 5000 + -right.Y && 5000 + left.Y == 5000 + -right.Z && 5000 + left.Z == 5000 + -right.X
               || 5000 + left.X == 5000 + right.Y && 5000 + left.Y == 5000 + -right.Z && 5000 + left.Z == 5000 + -right.X

               || 5000 + left.X == 5000 + -right.Z && 5000 + left.Y == 5000 + right.X && 5000 + left.Z == 5000 + right.Y
               || 5000 + left.X == 5000 + right.Z && 5000 + left.Y == 5000 + right.X && 5000 + left.Z == 5000 + right.Y
               || 5000 + left.X == 5000 + -right.Z && 5000 + left.Y == 5000 + -right.X && 5000 + left.Z == 5000 + right.Y
               || 5000 + left.X == 5000 + right.Z && 5000 + left.Y == 5000 + -right.X && 5000 + left.Z == 5000 + right.Y
               || 5000 + left.X == 5000 + -right.Z && 5000 + left.Y == 5000 + right.X && 5000 + left.Z == 5000 + -right.Y
               || 5000 + left.X == 5000 + right.Z && 5000 + left.Y == 5000 + right.X && 5000 + left.Z == 5000 + -right.Y
               || 5000 + left.X == 5000 + -right.Z && 5000 + left.Y == 5000 + -right.X && 5000 + left.Z == 5000 + -right.Y
               || 5000 + left.X == 5000 + right.Z && 5000 + left.Y == 5000 + -right.X && 5000 + left.Z == 5000 + -right.Y;
        // ReSharper restore CompareOfFloatsByEqualityOperator
    }
}