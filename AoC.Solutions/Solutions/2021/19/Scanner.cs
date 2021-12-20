using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2021._19;

public class Scanner
{
    public int Id { get; }

    public Point Position { get; set; }

    private List<Point> Beacons { get; }

    private List<(Point Distance, int Beacon1, int Beacon2)> Distances
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

    private List<(Point Distance, int Beacon1, int Beacon2)> _distances;

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
    }

    private (List<Point> OriginBeacons, List<Point> TargetBeacons) GetMatchingDistances(Scanner origin)
    {
        // TODO: This could really be optimised
        var originMatchIndexes = new List<int>();

        var targetMatchIndexes = new List<int>();

        for (var ob = 0; ob < origin.Distances.Count; ob++)
        {
            if (originMatchIndexes.Contains(ob))
            {
                continue;
            }

            // Why does b starting at ob + 1 break things?
            for (var b = 0; b < Distances.Count; b++)
            {
                if (targetMatchIndexes.Contains(b))
                {
                    continue;
                }

                // TODO: reduce dereferencing
                if (CheckDistancesMatchRegardlessOfOrientation(origin.Distances[ob].Distance, Distances[b].Distance))
                {
                    originMatchIndexes.Add(origin.Distances[ob].Beacon1);

                    originMatchIndexes.Add(origin.Distances[ob].Beacon2);

                    targetMatchIndexes.Add(Distances[b].Beacon1);

                    targetMatchIndexes.Add(Distances[b].Beacon2);
                }
            }
        }

        originMatchIndexes = originMatchIndexes.Distinct().ToList();

        targetMatchIndexes = targetMatchIndexes.Distinct().ToList();

        var originBeacons = new List<Point>();

        originMatchIndexes.ForEach(i => originBeacons.Add(origin.Beacons[i]));

        var targetBeacons = new List<Point>();

        targetMatchIndexes.ForEach(i => targetBeacons.Add(Beacons[i]));

        Console.WriteLine($"{originMatchIndexes.Count} == {targetMatchIndexes.Count}");

        return (originBeacons, targetBeacons);
    }

    // TODO: Rename if this works...
    private static bool CheckDistancesMatchRegardlessOfOrientation(Point left, Point right)
    {
        return left.X == right.X && left.Y == right.Y && left.Z == right.Z;

               //|| left.X == -right.X && left.Y == right.Y && left.Z == right.Z
               //|| left.X == right.X && left.Y == -right.Y && left.Z == right.Z
               //|| left.X == -right.X && left.Y == -right.Y && left.Z == right.Z
               //|| left.X == right.X && left.Y == right.Y && left.Z == -right.Z
               //|| left.X == -right.X && left.Y == right.Y && left.Z == -right.Z
               //|| left.X == right.X && left.Y == -right.Y && left.Z == -right.Z
               //|| left.X == -right.X && left.Y == -right.Y && left.Z == -right.Z;
    }

    private void CalculateDistances()
    {
        _distances = new List<(Point Distance, int Beacon1, int Beacon2)>();

        for (var ob = 0; ob < Beacons.Count; ob++)
        {
            for (var b = ob + 1; b < Beacons.Count; b++)
            {
                var distance = new Point(Math.Abs(Beacons[ob].X - Beacons[b].X), Math.Abs(Beacons[ob].Y - Beacons[b].Y), Math.Abs(Beacons[ob].Z - Beacons[b].Z));

                _distances.Add((distance, ob, b));

                //Console.WriteLine(Beacons[ob]);

                //Console.WriteLine($"{Beacons[b]} => {distance}");

                //Console.WriteLine();
            }
        }
    }
}