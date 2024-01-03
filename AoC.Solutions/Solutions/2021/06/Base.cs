using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2021._06;

public abstract class Base : Solution
{
    public override string Description => "Lanternfish parthenogenesis";

    public string GetAnswer(int days)
    {
        var fish = Input[0].Split(',').Select(int.Parse).ToList();

        var fishDays = new long[9];

        fish.ForEach(f => fishDays[f]++);

        for (var d = 0; d < days; d++)
        {
            var append = fishDays[0];

            for (var f = 0; f < 8; f++)
            {
                fishDays[f] = fishDays[f + 1];
            }

            fishDays[6] += append;

            fishDays[8] = append;
        }

        return fishDays.Sum().ToString();
    }
}