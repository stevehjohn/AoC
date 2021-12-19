using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._19;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly Dictionary<int, Scanner> _scanners = new();

    public override string GetAnswer()
    {
        ParseInput();

        return "TESTING";
    }

    private void ParseInput()
    {
        var scanner = new Scanner(0);

        var scannerIndex = 0;

        foreach (var line in Input)
        {
            if (line.StartsWith("---"))
            {
                scanner = new Scanner(scannerIndex);

                _scanners.Add(scannerIndex, scanner);

                scannerIndex++;

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