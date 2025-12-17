using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2020._05;

public abstract class Base : Solution
{
    public override string Description => "Boarding parse";

    protected static (int Row, int Column) GetSeatId(string input)
    {
        var row = ParseBinarySpace(input[..7]);

        var column = ParseBinarySpace(input[7..]);

        return (Row: row, Column: column);
    }

    private static int ParseBinarySpace(string input)
    {
        var value = 0;

        for (var i = 0; i < input.Length; i++)
        {
            if (input[i] is 'B' or 'R')
            {
                value |= 1 << (input.Length - i - 1);
            }
        }
        
        return value;
    }
}