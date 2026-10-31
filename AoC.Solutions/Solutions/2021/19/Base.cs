using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2021._19;

public abstract partial class Base : Solution
{
    public override string Description => "3D scanners and beacons";

    private static readonly Rotation[] Rotations = CreateRotations();

    private static Rotation[] CreateRotations()
    {
        var result = new List<Rotation>(24);

        for (var a = 0; a < 3; a++)
        {
            for (var b = 0; b < 3; b++)
            {
                if (a == b)
                {
                    continue;
                }

                var c = 3 - a - b;

                var parity = (a > b ? -1 : 1) * (a > c ? -1 : 1) * (b > c ? -1 : 1);

                foreach (var sx in new[] { -1, 1 })
                {
                    foreach (var sy in new[] { -1, 1 })
                    {
                        var sz = parity * sx * sy;

                        result.Add(new Rotation(a, b, c, sx, sy, sz));
                    }
                }
            }
        }

        return [.. result];
    }

    private static Dictionary<int, int> DistanceCounts(Vector[] points)
    {
        var counts = new Dictionary<int, int>();

        for (var i = 0; i < points.Length; i++)
        {
            for (var j = i + 1; j < points.Length; j++)
            {
                var distance = points[i].SquaredDistance(points[j]);
                
                counts[distance] = counts.GetValueOrDefault(distance) + 1;
            }
        }

        return counts;
    }

    private static bool PotentialOverlap(Scan a, Scan b)
    {
        var shared = 0;
        foreach (var (distance, count) in a.Distances)
        {
            shared += Math.Min(count, b.Distances.GetValueOrDefault(distance));
            
            if (shared >= 66)
            {
                return true;
            } 
        }

        return false;
    }

    private static bool TryAlign(Scan anchored, Scan candidate)
    {
        var fixedPoints = anchored.Global!;
        
        var fixedSet = new HashSet<Vector>(fixedPoints);
        
        var offsets = new Dictionary<Vector, int>(fixedPoints.Length * candidate.Local.Length);

        foreach (var rotated in candidate.Rotated)
        {
            offsets.Clear();
            
            foreach (var from in fixedPoints)
            {
                foreach (var to in rotated)
                {
                    var offset = from - to;

                    var count = offsets.GetValueOrDefault(offset) + 1;

                    if (count < 12)
                    {
                        offsets[offset] = count;

                        continue;
                    }

                    var overlaps = 0;

                    foreach (var p in rotated)
                    {
                        if (fixedSet.Contains(p + offset)) overlaps++;
                    }

                    if (overlaps < 12)
                    {
                        continue;
                    }

                    candidate.Position = offset;

                    candidate.Global = new Vector[rotated.Length];

                    for (var i = 0; i < rotated.Length; i++)
                    {
                        candidate.Global[i] = rotated[i] + offset;
                    }

                    return true;
                }
            }
        }

        return false;
    }

    protected (int BeaconCount, int MaxScannerDistance) SolveScanners()
    {
        var scans = new List<Scan>();

        List<Vector> current = null;

        foreach (var line in Input)
        {
            if (line.StartsWith("---"))
            {
                if (current != null)
                {
                    scans.Add(CreateScan(current));
                }

                current = [];
            }
            else if (! string.IsNullOrWhiteSpace(line))
            {
                var parts = line.Split(',');

                current!.Add(new Vector(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2])));
            }
        }

        if (current != null)
        {
            scans.Add(CreateScan(current));
        }

        if (scans.Count == 0) throw new InvalidOperationException("No scanners found.");

        scans[0].Global = scans[0].Local;

        scans[0].Position = new Vector(0, 0, 0);

        var pending = new Queue<int>();

        pending.Enqueue(0);

        var unaligned = scans.Count - 1;

        while (pending.Count > 0)
        {
            var origin = scans[pending.Dequeue()];

            for (var j = 1; j < scans.Count; j++)
            {
                var candidate = scans[j];

                if (candidate.Global != null || ! PotentialOverlap(origin, candidate))
                {
                    continue;
                }

                if (! TryAlign(origin, candidate))
                {
                    continue;
                }

                pending.Enqueue(j);

                unaligned--;
            }
        }

        if (unaligned != 0)
        {
            throw new InvalidOperationException($"Unable to align {unaligned} scanners.");
        }

        var allBeacons = new HashSet<Vector>();

        foreach (var scan in scans)
        {
            allBeacons.UnionWith(scan.Global!);
        }

        var maxDistance = 0;

        for (var i = 0; i < scans.Count; i++)
        {
            for (var j = i + 1; j < scans.Count; j++)
            {
                maxDistance = Math.Max(maxDistance, scans[i].Position.Manhattan(scans[j].Position));
            }
        }

        return (allBeacons.Count, maxDistance);
    }

    private static Scan CreateScan(List<Vector> points)
    {
        var local = points.ToArray();

        var rotated = new Vector[Rotations.Length][];

        for (var r = 0; r < Rotations.Length; r++)
        {
            rotated[r] = new Vector[local.Length];
            
            for (var i = 0; i < local.Length; i++)
            {
                rotated[r][i] = Rotations[r].Apply(local[i]);
            }
        }

        return new Scan
        {
            Local = local,
            Rotated = rotated,
            Distances = DistanceCounts(local)
        };
    }
}