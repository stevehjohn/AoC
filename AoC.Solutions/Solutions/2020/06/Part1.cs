using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._06;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var uniqueCharacters = new HashSet<char>();

        var total = 0;

        foreach (var line in Input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                total += uniqueCharacters.Count;

                uniqueCharacters.Clear();

                continue;
            }

            line.ToList().ForEach(c => uniqueCharacters.Add(c));
        }

        total += uniqueCharacters.Count;

        return total.ToString();
    }
}