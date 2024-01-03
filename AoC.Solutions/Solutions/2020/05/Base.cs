using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2020._05;

public abstract class Base : Solution
{
    public override string Description => "Boarding parse";

    protected static (int Row, int Column) GetSeatId(string input)
    {
        var row = ParseBinarySpace(input[..7], 127);

        var column = ParseBinarySpace(input[7..], 7);

        return (Row: row, Column: column);
    }

    protected static int ParseBinarySpace(string input, int max)
    {
        var min = 0;

        foreach (var c in input)
        {
            var halved = (int) Math.Ceiling((max - min) / 2d);

            if (c is 'F' or 'L') // Lower
            {
                max -= halved;

                continue;
            }

            min += halved;
        }
        
        return min;
    }
}