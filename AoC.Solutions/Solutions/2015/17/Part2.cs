using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._17;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        const int target = 150;

        var minimum = new int[target + 1];
        
        var ways = new int[target + 1];

        Array.Fill(minimum, int.MaxValue);

        minimum[0] = 0;
        
        ways[0] = 1;

        foreach (var line in Input)
        {
            var capacity = int.Parse(line);

            for (var volume = target; volume >= capacity; volume--)
            {
                var previous = minimum[volume - capacity];

                if (previous == int.MaxValue)
                {
                    continue;
                }

                var count = previous + 1;

                if (count < minimum[volume])
                {
                    minimum[volume] = count;
                    
                    ways[volume] = ways[volume - capacity];
                }
                else if (count == minimum[volume])
                {
                    ways[volume] += ways[volume - capacity];
                }
            }
        }

        return ways[target].ToString();
    }
}