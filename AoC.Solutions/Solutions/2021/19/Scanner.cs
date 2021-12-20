﻿using AoC.Solutions.Common;

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

        foreach (var beacon in Beacons)
        {
            var positiveSpacePoint = new PointDouble(xBase + beacon.X, yBase + beacon.Y, zBase + beacon.Z);

            _normalisedBeacons.Add(positiveSpacePoint);
        }

        var xCentre = Beacons.Average(b => b.X);

        var yCentre = Beacons.Average(b => b.Y);

        var zCentre = Beacons.Average(b => b.Z);

        foreach (var beacon in NormalisedBeacons)
        {
            beacon.X = xCentre - beacon.X;

            beacon.Y = yCentre - beacon.Y;
            
            beacon.Z = zCentre - beacon.Z;
        }
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

                if (matchedPositions.Count == 3)
                {
                    break;
                }
            }

            if (matchedPositions.Count == 3)
            {
                break;
            }
        }
    }

    private static bool MatchBeaconPosition(PointDouble left, PointDouble right)
    {
        return left.X == right.X && left.Y == right.Y && left.Z == right.Z;
    }
}