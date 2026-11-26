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

        var low = (int) (AvailableOre / oreForOne);

        var high = low * 2;

        while (true)
        {
            ResetStock();

            if (CreateFuel(high) > AvailableOre)
            {
                break;
            }

            high *= 2;
        }

        while (low + 1 < high)
        {
            var middle = low + (high - low) / 2;

            ResetStock();

            if (CreateFuel(middle) <= AvailableOre)
            {
                low = middle;
            }
            else
            {
                high = middle;
            }
        }

        return low.ToString();
    }
}