using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._06;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var allCharacters = new HashSet<char>();

        for (var c = 'a'; c <= 'z'; c++)
        {
            allCharacters.Add(c);
        }

        var commonCharacters = new HashSet<char>(allCharacters);

        var total = 0;

        foreach (var line in Input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                total += commonCharacters.Count;

                commonCharacters = [..allCharacters];

                continue;
            }

            var lineCharacters = line.ToHashSet();

            commonCharacters = commonCharacters.Intersect(lineCharacters).ToHashSet();
        }

        total += commonCharacters.Count;

        return total.ToString();
    }
}