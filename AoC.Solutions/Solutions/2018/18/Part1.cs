using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._18;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        for (var i = 0; i < 10; i++)
        {
            RunCycle();
        }

        return GetResourceValue().ToString();
    }

    private int GetResourceValue()
    {
        var wood = 0;

        var yard = 0;

        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                if (Map[x, y] == '|')
                {
                    wood++;

                    continue;
                }

                if (Map[x, y] == '#')
                {
                    yard++;
                }
            }
        }

        return wood * yard;
    }
}