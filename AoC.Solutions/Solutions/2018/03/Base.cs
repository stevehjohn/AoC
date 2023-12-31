using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._03;

[UsedImplicitly]
public abstract class Base : Solution
{
    public override string Description => "Fabric overlap";

    protected const int FabricSize = 1_000;

    protected readonly char[,] Fabric = new char[FabricSize, FabricSize];

    protected void FillArea(Point topLeft, Point size)
    {
        for (var y = 0; y < size.Y; y++)
        {
            for (var x = 0; x < size.X; x++)
            {
                Fabric[topLeft.X + x, topLeft.Y + y] = Fabric[topLeft.X + x, topLeft.Y + y] == '\0' ? '#' : 'X';
            }
        }
    }

    protected static (int ClaimId, Point TopLeft, Point Size) ParseLine(string line)
    {
        var split = line.Split('@', StringSplitOptions.TrimEntries);

        var claimId = int.Parse(split[0][1..]);

        split = split[1].Split(':', StringSplitOptions.TrimEntries);

        return (claimId, GetPoint(split[0], ','), GetPoint(split[1], 'x'));
    }

    private static Point GetPoint(string data, char delimiter)
    {
        var split = data.Split(delimiter, StringSplitOptions.TrimEntries);

        return new Point(int.Parse(split[0]), int.Parse(split[1]));
    }
}