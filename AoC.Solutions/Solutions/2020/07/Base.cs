using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2020._07;

public abstract class Base : Solution
{
    public override string Description => "Recursive bag packing";

    protected List<(string Container, string Contains, int Count)> BagData = new();

    protected void ParseInput()
    {
        foreach (var line in Input)
        {
            var split = line.Split("bags contain", StringSplitOptions.TrimEntries);

            if (split[1].StartsWith("no other"))
            {
                continue;
            }

            var container = split[0];

            var containsList = split[1][..^1].Split(',', StringSplitOptions.TrimEntries);

            foreach (var item in containsList)
            {
                var count = item[0] - '0';

                var bag = item[2..^(item[^1] == 's' ? 5 : 4)];

                BagData.Add((Container: container, Contains: bag, Count: count));
            }
        }
    }
}