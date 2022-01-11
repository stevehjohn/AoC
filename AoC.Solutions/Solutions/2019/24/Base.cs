using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2019._24;

public abstract class Base : Solution
{
    public override string Description => "Wastl's game of life";

    protected int ParseInput()
    {
        var result = 0;

        var bit = 1;

        foreach (var line in Input)
        {
            for (var x = 0; x < 5; x++)
            {
                result |= line[x] == '#' ? bit : 0;
         
                bit <<= 1;
            }
        }

        return result;
    }
}