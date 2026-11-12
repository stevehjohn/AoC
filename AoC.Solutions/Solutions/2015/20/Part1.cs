using AoC.Solutions.Extensions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._20;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var target = int.FastParse(Input[0]);

        var limit = (target + 9) / 10;

        var presents = new int[limit + 1];

        for (var elf = 1; elf <= limit; elf++)
        {
            var delivery = elf * 10;

            for (var house = elf; house <= limit; house += elf)
            {
                presents[house] += delivery;
            }
        }

        for (var house = 1; house <= limit; house++)
        {
            if (presents[house] < target)
            {
                continue;
            }

            File.WriteAllText("2015.20.1.result", $"{house}");

            return house.ToString();
        }

        throw new InvalidOperationException("Solution not found.");
    }
}