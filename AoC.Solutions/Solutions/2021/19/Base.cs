using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2021._19;

public abstract class Base : Solution
{
    public override string Description => "3D scanners and beacons";

    protected readonly Dictionary<int, Scanner> Scanners = new();

    protected void ParseInput()
    {
        var scanner = new Scanner
                      {
                          Position = new Point()
                      };

        Scanners.Add(0, scanner);

        var scannerIndex = 0;

        foreach (var line in Input.Skip(1))
        {
            if (line.StartsWith("---"))
            {
                scannerIndex++;

                scanner = new Scanner();

                Scanners.Add(scannerIndex, scanner);

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