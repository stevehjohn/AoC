using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._03;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        foreach (var line in Input)
        {
            var data = ParseLine(line);

            FillArea(data.TopLeft, data.Size);
        }

        return CountOverlaps().ToString();
    }

    private int CountOverlaps()
    {
        var count = 0;

        for (var y = 0; y < FabricSize; y++)
        {
            for (var x = 0; x < FabricSize; x++)
            {
                if (Fabric[x, y] == 'X')
                {
                    count++;
                }
            }
        }

        return count;
    }
}