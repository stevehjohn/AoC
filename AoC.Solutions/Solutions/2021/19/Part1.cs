using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._19;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly Dictionary<int, List<HashSet<string>>> _scanners = new();

    public override string GetAnswer()
    {
        ParseInput();

        var scannerCount = _scanners.Count;

        var x = 0;

        for (var s1 = 0; s1 < scannerCount; s1++)
        {
            var scanner1 = _scanners[s1];

            var scanner1BeaconCount = scanner1.Count;

            for (var b1 = 0; b1 < scanner1BeaconCount; b1++)
            {
                var scanner1Beacon = scanner1[b1];

                for (var s2 = 0; s2 < scannerCount; s2++)
                {
                    if (s1 == s2)
                    {
                        continue;
                    }

                    var scanner2 = _scanners[s1];

                    var scanner2BeaconCount = scanner1.Count;

                    for (var b2 = 0; b2 < scanner2BeaconCount; b2++)
                    {
                        var scanner2Beacon = scanner2[b2];

                        var common = scanner1Beacon.Intersect(scanner2Beacon).ToList();

                        if (common.Count > 0)
                        {
                            Console.WriteLine(common.Count);
                        }
                    }
                }
            }
        }

        return "TESTING";
    }

    private void ParseInput()
    {
        List<HashSet<string>> beacons = new List<HashSet<string>>();

        var scanner = 0;

        foreach (var line in Input)
        {
            if (line.StartsWith("---"))
            {
                beacons = new List<HashSet<string>>();

                _scanners.Add(scanner, beacons);

                scanner++;

                continue;
            }

            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            beacons.Add(GetOrientationHashes(line));
        }
    }

    private HashSet<string> GetOrientationHashes(string line)
    {
        var components = line.Split(',').Select(int.Parse).ToArray();

        var hashes = new HashSet<string>();

        hashes.Add(FormatCoords(components[0], components[1], components[2], -1, 1, 1));

        hashes.Add(FormatCoords(components[0], components[1], components[2], 1, 1, 1));

        hashes.Add(FormatCoords(components[0], components[1], components[2], -1, -1, 1));

        hashes.Add(FormatCoords(components[0], components[1], components[2], 1, -1, 1));

        hashes.Add(FormatCoords(components[0], components[1], components[2], -1, 1, -1));

        hashes.Add(FormatCoords(components[0], components[1], components[2], 1, 1, -1));

        hashes.Add(FormatCoords(components[0], components[1], components[2], -1, -1, -1));

        hashes.Add(FormatCoords(components[0], components[1], components[2], 1, -1, -1));

        hashes.Add(FormatCoords(components[1], components[2], components[0], -1, 1, 1));

        hashes.Add(FormatCoords(components[1], components[2], components[0], 1, 1, 1));

        hashes.Add(FormatCoords(components[1], components[2], components[0], -1, -1, 1));

        hashes.Add(FormatCoords(components[1], components[2], components[0], 1, -1, 1));

        hashes.Add(FormatCoords(components[1], components[2], components[0], -1, 1, -1));

        hashes.Add(FormatCoords(components[1], components[2], components[0], 1, 1, -1));

        hashes.Add(FormatCoords(components[1], components[2], components[0], -1, -1, -1));

        hashes.Add(FormatCoords(components[1], components[2], components[0], 1, -1, -1));

        hashes.Add(FormatCoords(components[2], components[0], components[1], -1, 1, 1));

        hashes.Add(FormatCoords(components[2], components[0], components[1], 1, 1, 1));

        hashes.Add(FormatCoords(components[2], components[0], components[1], -1, -1, 1));

        hashes.Add(FormatCoords(components[2], components[0], components[1], 1, -1, 1));

        hashes.Add(FormatCoords(components[2], components[0], components[1], -1, 1, -1));

        hashes.Add(FormatCoords(components[2], components[0], components[1], 1, 1, -1));

        hashes.Add(FormatCoords(components[2], components[0], components[1], -1, -1, -1));

        hashes.Add(FormatCoords(components[2], components[0], components[1], 1, -1, -1));

        return hashes;
    }

    private string FormatCoords(int x, int y, int z, int flipX, int flipY, int flipZ)
    {
        return $"{x * flipX},{y * flipY},{z * flipZ}";
    }
}