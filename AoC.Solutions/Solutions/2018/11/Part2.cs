using AoC.Solutions.Common;
using AoC.Solutions.Exceptions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._11;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        CalculateCellPowers(ParseInput());

        var max = 0;

        var maxSize = 0;

        Point position = null;

        for (var s = 1; s <= GridSize; s++)
        {
            var result = GetMaxPower(s);

            if (result.Max > max)
            {
                max = result.Max;

                maxSize = s;

                position = result.Position;
            }
        }
        
        if (position == null)
        {
            throw new PuzzleException("Solution not found.");
        }

        return $"{position.X},{position.Y},{maxSize}";
    }
}