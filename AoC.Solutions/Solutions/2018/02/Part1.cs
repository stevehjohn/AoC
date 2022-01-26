using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._02;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var twos = 0;

        var threes = 0;

        foreach (var line in Input)
        {
            var result = CheckLine(line);

            twos += result.Twos;

            threes += result.Threes;
        }

        return (twos * threes).ToString();
    }

    private static (int Twos, int Threes) CheckLine(string data)
    {
        var counts = new Dictionary<char, int>();

        foreach (var c in data)
        {
            if (! counts.TryAdd(c, 1))
            {
                counts[c]++;
            }
        }

        return (counts.Any(c => c.Value == 2) ? 1 : 0, counts.Any(c => c.Value == 3) ? 1 : 0);
    }
}