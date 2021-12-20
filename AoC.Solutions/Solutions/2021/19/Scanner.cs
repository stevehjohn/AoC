using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2021._19;

public class Scanner
{
    public int Id { get; }

    public Point Position { get; set; }

    private List<Point> Beacons { get; }

    private List<Point> Distances
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

    private List<Point> _distances;

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
        var matchedDistances = GetMatchingDistances(origin);
    }

    private List<Point> GetMatchingDistances(Scanner origin)
    {
        var matches = new List<Point>();

        var c = 0;

        for (var ob = 0; ob < origin.Distances.Count; ob++)
        {
            for (var b = ob; b < Distances.Count; b++)
            {
                if (CheckDistancesMatchRegardlessOfOrientation(origin.Distances[ob], Distances[b]))
                {
                    // Which distance do we store?
                    c++;
                }
            }
        }

        Console.WriteLine(c);

        return matches;
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
        _distances = new List<Point>();

        for (var ob = 0; ob < Beacons.Count; ob++)
        {
            for (var b = ob + 1; b < Beacons.Count; b++)
            {
                var distance = new Point(Beacons[ob].X - Beacons[b].X, Beacons[ob].Y - Beacons[b].Y, Beacons[ob].Z - Beacons[b].Z);

                _distances.Add(distance);

                //Console.WriteLine(Beacons[ob]);

                //Console.WriteLine($"{Beacons[b]} => {distance}");

                //Console.WriteLine();
            }
        }
    }
}