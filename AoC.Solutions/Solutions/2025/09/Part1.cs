using AoC.Solutions.Libraries;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._09;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var largest = 0L;

        for (var l = 0; l < Coordinates.Length - 1; l++)
        {
            for (var r = l + 1; r < Coordinates.Length; r++)
            {
                var left = Coordinates[l];

                var right = Coordinates[r];

                var area = Measurement.AreaInCells(left, right);

                if (area > largest)
                {
                    largest = area;
                }
            }
        }

        return largest.ToString();
    }
}