using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._05;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var highest = 0;

        foreach (var line in Input)
        {
            var seatLocation = GetSeatId(line);

            var seatId = seatLocation.Row * 8 + seatLocation.Column;

            if (seatId > highest)
            {
                highest = seatId;
            }
        }

        return highest.ToString();
    }

    private (int Row, int Column) GetSeatId(string input)
    {
        var row = ParseBinarySpace(input[..7], 127);

        var column = ParseBinarySpace(input[7..], 7);

        return (Row: row, Column: column);
    }

    private int ParseBinarySpace(string input, int max)
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