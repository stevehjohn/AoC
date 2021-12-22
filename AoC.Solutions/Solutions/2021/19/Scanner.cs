﻿using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2021._19;

public class Scanner
{
    public Point Position { get; set; }

    public int BeaconCount => Beacons.Count;

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

    public Scanner()
    {
        Beacons = new List<Point>();
    }

    public void AddBeacon(int x, int y, int z)
    {
        Beacons.Add(new Point(x, y, z));
    }

    public void RemoveMatchingBeacons(Scanner origin)
    {
        var matchingBeacons = GetMatchingDistances(origin);

        var beaconPairs = ResolveMatchingBeacons(matchingBeacons);

        Beacons.RemoveAll(b => beaconPairs.Any(p => p.Beacon2.Equals(b)));
    }

    public void LocateRelativeTo(Scanner origin)
    {
        var matchingBeacons = GetMatchingDistances(origin);

        var beaconPairs = ResolveMatchingBeacons(matchingBeacons);

        FindTranslation(beaconPairs.Take(12).ToList());
    }

    private void FindTranslation(List<Pair> pairs)
    {
        // Find a translation that works for all pairs (or 12, or something)
        var allTranslations = new Dictionary<Point, int>();

        foreach (var pair in pairs)
        {
            var translations = GetTranslations(pair.Beacon1, pair.Beacon2);

            foreach (var translation in translations)
            {
                if (allTranslations.ContainsKey(translation))
                {
                    allTranslations[translation]++;
                }
                else
                {
                    allTranslations.Add(translation, 1);
                }
            }
        }

        var x = allTranslations.Where(t => Math.Abs(t.Key.X) == 68 && Math.Abs(t.Key.Y) == 1246 && Math.Abs(t.Key.Z) == 43);

        foreach (var p in x)
        {
            Console.WriteLine(p.Key);
        }
    }

    private static List<Point> GetTranslations(Point left, Point right)
    {
        var translations = new List<Point>();

        var rotations = GetRotations(right);

        foreach (var rotation in rotations)
        {
            translations.Add(new Point(left.X - rotation.X, left.Y + rotation.Y, left.Z + rotation.Z));
            translations.Add(new Point(left.X - rotation.X, left.Y - rotation.Y, left.Z + rotation.Z));
            translations.Add(new Point(left.X - rotation.X, left.Y + rotation.Y, left.Z - rotation.Z));
            translations.Add(new Point(left.X - rotation.X, left.Y - rotation.Y, left.Z - rotation.Z));
            translations.Add(new Point(left.X + rotation.X, left.Y + rotation.Y, left.Z + rotation.Z));
            translations.Add(new Point(left.X + rotation.X, left.Y - rotation.Y, left.Z + rotation.Z));
            translations.Add(new Point(left.X + rotation.X, left.Y + rotation.Y, left.Z - rotation.Z));
            translations.Add(new Point(left.X + rotation.X, left.Y - rotation.Y, left.Z - rotation.Z));
        }

        return translations;
    }

    private static List<Point> GetRotations(Point right)
    {
        var rotations = new List<Point>();

        rotations.Add(new Point(right.X, right.Y, right.Z));
        rotations.Add(new Point(right.X, right.Z, right.Y));
        rotations.Add(new Point(right.X, right.Y, -right.Z));
        rotations.Add(new Point(right.X, right.Z, -right.Y));
        rotations.Add(new Point(right.X, -right.Y, right.Z));
        rotations.Add(new Point(right.X, -right.Z, right.Y));
        rotations.Add(new Point(-right.X, right.Y, right.Z));
        rotations.Add(new Point(-right.X, right.Z, right.Y));

        rotations.Add(new Point(right.Y, right.X, right.Z));
        rotations.Add(new Point(right.Y, right.Z, right.X));
        rotations.Add(new Point(right.Y, right.X, -right.Z));
        rotations.Add(new Point(right.Y, right.Z, -right.X));
        rotations.Add(new Point(right.Y, -right.X, right.Z));
        rotations.Add(new Point(right.Y, -right.Z, right.X));
        rotations.Add(new Point(-right.Y, right.X, right.Z));
        rotations.Add(new Point(-right.Y, right.Z, right.X));

        rotations.Add(new Point(right.Z, right.X, right.Y));
        rotations.Add(new Point(right.Z, right.Y, right.X));
        rotations.Add(new Point(right.Z, right.X, -right.Y));
        rotations.Add(new Point(right.Z, right.Y, -right.X));
        rotations.Add(new Point(right.Z, -right.X, right.Y));
        rotations.Add(new Point(right.Z, -right.Y, right.X));
        rotations.Add(new Point(-right.Z, right.X, right.Y));
        rotations.Add(new Point(-right.Z, right.Y, right.X));

        return rotations;
    }

    private static List<Pair> ResolveMatchingBeacons(List<DistancePair> matchingBeacons)
    {
        var pairs = new List<Pair>();

        var distinctOrigins = matchingBeacons.Select(p => p.Origin.Beacon1).Distinct().Union(matchingBeacons.Select(p => p.Origin.Beacon2).Distinct());

        foreach (var originBeacon in distinctOrigins)
        {
            var candidates = new List<Point>();

            foreach (var candidatePair in matchingBeacons)
            {
                if (! (originBeacon.Equals(candidatePair.Origin.Beacon1) || originBeacon.Equals(candidatePair.Origin.Beacon2)))
                {
                    continue;
                }

                if (candidates.Any(c => Equals(c, candidatePair.Target.Beacon1)))
                {
                    pairs.Add(new Pair(originBeacon, candidatePair.Target.Beacon1));

                    break;
                }

                if (candidates.Any(c => Equals(c, candidatePair.Target.Beacon2)))
                {
                    pairs.Add(new Pair(originBeacon, candidatePair.Target.Beacon2));

                    break;
                }

                candidates.Add(candidatePair.Target.Beacon1);
                
                candidates.Add(candidatePair.Target.Beacon2);
            }
        }

        return pairs;
    }

    private List<DistancePair> GetMatchingDistances(Scanner origin)
    {
        var distances = new List<DistancePair>();

        for (var ob = 0; ob < origin.Distances.Count; ob++)
        {
            var originDistance = origin.Distances[ob];

            // TODO: Why does b starting at ob + 1 break things?
            for (var b = 0; b < Distances.Count; b++)
            {
                var targetDistance = Distances[b];

                if (CheckDistanceMatch(originDistance.Delta, targetDistance.Delta))
                {
                    distances.Add(new DistancePair(origin.Distances[ob], Distances[b]));
                }
            }
        }

        return distances;
    }

    private static bool CheckDistanceMatch(Point left, Point right)
    {
        return left.X == right.X && left.Y == right.Y && left.Z == right.Z
               || left.X == right.Y && left.Y == right.X && left.Z == right.Z
               || left.X == right.Z && left.Y == right.X && left.Z == right.Y
               || left.X == right.X && left.Y == right.Z && left.Z == right.Y
               || left.X == right.Y && left.Y == right.Z && left.Z == right.X
               || left.X == right.Z && left.Y == right.Y && left.Z == right.X;
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

                var distance = new Point(Math.Abs(outerBeacon.X - beacon.X), Math.Abs(outerBeacon.Y - beacon.Y), Math.Abs(outerBeacon.Z - beacon.Z));

                _distances.Add(new Distance(distance, outerBeacon, beacon));
            }
        }
    }
}