using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2020._05;

public abstract class Base : Solution
{
    public override string Description => "Boarding parse";

    protected static (int Row, int Column) GetSeatId(string input)
    {
        var row = ParseBinarySpace(input[..7], 64);

        var column = ParseBinarySpace(input[7..], 4);

        return (Row: row, Column: column);
    }

    private static int ParseBinarySpace(string input, int max)
    {
        var value = 0;

        for (var i = 0; i < input.Length; i++)
        {
            if (input[i] is 'B' or 'R')
            {
                value |= max >> i;
            }
        }
        
        return value;
    }
}