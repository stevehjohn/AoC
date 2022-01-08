using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._14;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var fuel = 0;

        var ore = 0L;

        while (true)
        {
            ore += CreateFuel();

            if (ore > 1_000_000_000_000)
            {
                break;
            }

            fuel++;
        }

        return fuel.ToString();
    }
}