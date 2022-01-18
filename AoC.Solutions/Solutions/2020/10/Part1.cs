using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._10;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseData();

        var voltage = 0;

        var ones = 0;

        var threes = 1;

        while (true)
        {
            var adapters = Data.Where(v => v > voltage && v < voltage + 4).ToList();

            if (adapters.Count == 0)
            {
                break;
            }

            var adapter = (int) adapters.Min();

            if (adapter - voltage == 3)
            {
                threes++;
            }

            if (adapter - voltage == 1)
            {
                ones++;
            }

            voltage = adapter;
        }

        return (ones * threes).ToString();
    }
}