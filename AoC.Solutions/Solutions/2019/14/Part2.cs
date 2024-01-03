using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._14;

[UsedImplicitly]
public class Part2 : Base
{
    private const long AvailableOre = 1_000_000_000_000;

    public override string GetAnswer()
    {
        ParseInput();

        var oreForOne = CreateFuel();

        var minEstimate = (int) (AvailableOre / oreForOne);

        var increment = minEstimate / 10;

        while (true)
        {
            ResetStock();

            var ore = CreateFuel(minEstimate);

            if (ore > AvailableOre)
            {
                ResetStock();

                if (CreateFuel(minEstimate - 1) < AvailableOre)
                {
                    break;
                }

                minEstimate -= increment;

                increment /= 2;
            }

            minEstimate += increment;
        }

        return (minEstimate - 1).ToString();
    }
}