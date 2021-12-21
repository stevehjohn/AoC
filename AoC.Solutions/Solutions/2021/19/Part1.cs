using AoC.Solutions.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._19;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly Dictionary<int, Scanner> _scanners = new();

    public override string GetAnswer()
    {
        ParseInput();

        // TODO: Can this be slightly optimised also?
        for (var s1 = 0; s1 < _scanners.Count; s1++)
        {
            var originScanner = _scanners[s1];

            for (var s2 = s1 + 1; s2 < _scanners.Count; s2++)
            {
                originScanner.RemoveMatchingBeacons(_scanners[s2]);
            }
        }

        var beaconCount = 0;

        for (var s = 0; s < _scanners.Count; s++)
        {
            beaconCount += _scanners[s].BeaconCount;
        }

        return beaconCount.ToString();
    }

    private void ParseInput()
    {
        var scanner = new Scanner(0)
                      {
                          Position = new Point()
                      };

        _scanners.Add(0, scanner);

        var scannerIndex = 0;

        foreach (var line in Input.Skip(1))
        {
            if (line.StartsWith("---"))
            {
                scannerIndex++;

                scanner = new Scanner(scannerIndex);

                _scanners.Add(scannerIndex, scanner);

                continue;
            }

            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            var coordinates = line.Split(',').Select(int.Parse).ToArray();

            scanner.AddBeacon(coordinates[0], coordinates[1], coordinates[2]);
        }
    }
}