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
}