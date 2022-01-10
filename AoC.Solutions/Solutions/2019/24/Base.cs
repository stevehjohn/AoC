using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2019._24;

public abstract class Base : Solution
{
    public override string Description => "Wastl's game of life";

    protected bool[,] ParseInput()
    {
        var result = new bool[7, 7];

        var y = 1;

        foreach (var line in Input)
        {
            for (var x = 1; x < 6; x++)
            {
                result[x, y] = line[x - 1] == '#';
            }

            y++;
        }

        return result;
    }
}