using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._06;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var unique = new HashSet<char>();

        var total = 0;

        foreach (var line in Input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                total += unique.Count;

                unique.Clear();

                continue;
            }

            line.ToList().ForEach(c => unique.Add(c));
        }

        total += unique.Count;

        return total.ToString();
    }
}