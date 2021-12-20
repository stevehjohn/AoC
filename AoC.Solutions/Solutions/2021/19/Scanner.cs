using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2021._19;

public class Scanner
{
    private const int RequiredMatchesByScanner = 3; // TODO: Reset to proper value when working

    private const int RequiredMatchesForOrientation = 3;

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
        if (matchingBeacons != null)
        {
            foreach (var beacon in matchingBeacons)
            {
                //Console.WriteLine($"{beacon.Origin.Beacon1}                  {beacon.Target.Beacon1}");

                //Console.WriteLine($"    {beacon.Origin.Delta}         {beacon.Target.Delta}");

                //Console.WriteLine($"{beacon.Origin.Beacon2}                  {beacon.Target.Beacon2}");

                //Console.WriteLine();
            }
        }
    }

    private List<DistancePair> GetMatchingDistances(Scanner origin)
    {
        // TODO: Any more optimisations here? Getting 66 results for 12 matches seems ludicrous.
        var distances = new List<DistancePair>();

        var matchCount = 0;

        var matchesRequired = (RequiredMatchesByScanner - 1) * RequiredMatchesByScanner / 2;

        for (var ob = 0; ob < origin.Distances.Count; ob++)
        {
            var originDistance = origin.Distances[ob];

            //if (distances.Any(d => d.Origin == originDistance))
            //{
            //    continue;
            //}

            // TODO: Why does b starting at ob + 1 break things?
            for (var b = 0; b < Distances.Count; b++)
            {
                var targetDistance = Distances[b];

                //if (distances.Any(d => d.Target == targetDistance))
                //{
                //    continue;
                //}

                if (CheckDistancesMatchRegardlessOfOrientation(originDistance.Delta, targetDistance.Delta))
                {
                    if (distances.Count < RequiredMatchesForOrientation)
                    {
                        distances.Add(new DistancePair(origin.Distances[ob], Distances[b]));
                    }

                    matchCount++;

                    if (matchCount == matchesRequired)
                    {
                        return distances;
                    }
                }
            }
        }

        return null;
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