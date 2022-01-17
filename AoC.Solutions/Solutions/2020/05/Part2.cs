using AoC.Solutions.Exceptions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._05;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var ids = new List<int>();

        foreach (var line in Input)
        {
            var seatLocation = GetSeatId(line);

            var seatId = seatLocation.Row * 8 + seatLocation.Column;

            ids.Add(seatId);
        }

        ids = ids.OrderBy(i => i).ToList();

        for (var i = 0; i < ids.Count - 2; i++)
        {
            if (ids[i] != ids[i + 1] - 1)
            {
                return (ids[i] + 1).ToString();
            }
        }

        throw new PuzzleException("Solution not found.");
    }
}