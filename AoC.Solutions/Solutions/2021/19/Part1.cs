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

        for (var s1 = 0; s1 < _scanners.Count; s1++)
        {
            for (var s2 = 1; s2 < _scanners.Count; s2++)
            {
                if (s1 == s2 || _scanners[s1].Position != null && _scanners[s2].Position != null)
                {
                    continue;
                }

                _scanners[s2].TryGetPosition(_scanners[s1]);
            }
        }

        return "TESTING";
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