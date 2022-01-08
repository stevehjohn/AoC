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

        while (true)
        {
            ResetStock();

            var ore = CreateFuel(minEstimate);

            if (ore > AvailableOre)
            {
                break;
            }

            if (minEstimate % 100_000 == 0)
            {
                Console.WriteLine(minEstimate);
            }

            minEstimate++;
        }

        return (minEstimate - 1).ToString();
    }
}