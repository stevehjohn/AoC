using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2021._19;

public class Scanner
{
    public int Id { get; }

    public Point Position { get; set; }

    private List<Point> Beacons { get; }

    private List<Distance> Distances
    {
        get
        {
            if (_distances == null)
            {
                CalculateDistances();
            }

            return _distances;
        }
    }

    private List<Distance> _distances;

    public Scanner(int id)
    {
        Id = id;

        Beacons = new List<Point>();
    }

    public void AddBeacon(int x, int y, int z)
    {
        Beacons.Add(new Point(x, y, z));
    }

    public void TryGetPosition(Scanner origin)
    {
        var matchingBeacons = GetMatchingDistances(origin);

        // 3 distances should be enough to orient the scanner...
        // Then just one matching oriented beacon can be used for position?
    }

    private List<DistancePair> GetMatchingDistances(Scanner origin)
    {
        // TODO: Any more optimisations here?
        var distances = new List<DistancePair>();

        for (var ob = 0; ob < origin.Distances.Count; ob++)
        {
            var originDistance = origin.Distances[ob];

            //if (distances.Contains(originDistance))
            //{
            //    continue;
            //}

            // TODO: Why does b starting at ob + 1 break things?
            for (var b = 0; b < Distances.Count; b++)
            {
                var targetDistance = Distances[b];

                //if (distances.Contains(targetDistance))
                //{
                //    continue;
                //}

                // TODO: reduce dereferencing?
                if (CheckDistancesMatchRegardlessOfOrientation(originDistance.Delta, targetDistance.Delta))
                {
                    distances.Add(new DistancePair(origin.Distances[ob], Distances[b]));
                }
            }
        }

        return distances;
    }

    private static bool CheckDistancesMatchRegardlessOfOrientation(Point left, Point right)
    {
        return left.X == right.X && left.Y == right.Y && left.Z == right.Z
               || left.X == -right.X && left.Y == right.Y && left.Z == right.Z
               || left.X == right.X && left.Y == -right.Y && left.Z == right.Z
               || left.X == -right.X && left.Y == -right.Y && left.Z == right.Z
               || left.X == right.X && left.Y == right.Y && left.Z == -right.Z
               || left.X == -right.X && left.Y == right.Y && left.Z == -right.Z
               || left.X == right.X && left.Y == -right.Y && left.Z == -right.Z
               || left.X == -right.X && left.Y == -right.Y && left.Z == -right.Z;
    }

    private void CalculateDistances()
    {
        _distances = new List<Distance>();

        for (var ob = 0; ob < Beacons.Count; ob++)
        {
            for (var b = ob + 1; b < Beacons.Count; b++)
            {
                var outerBeacon = Beacons[ob];

                var beacon = Beacons[b];

                var distance = new Point(outerBeacon.X - beacon.X, outerBeacon.Y - beacon.Y, outerBeacon.Z - beacon.Z);

                _distances.Add(new Distance(distance, outerBeacon, beacon));

                //Console.WriteLine(Beacons[ob]);

                //Console.WriteLine($"{Beacons[b]} => {distance}");

                //Console.WriteLine();
            }
        }
    }
}