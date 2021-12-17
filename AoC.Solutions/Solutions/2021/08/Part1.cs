using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._08;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var outputs = Input.Select(s => s.Split('|', StringSplitOptions.TrimEntries)[1]);

        var total = 0;

        foreach (var output in outputs)
        {
            var digits = output.Split(' ', StringSplitOptions.TrimEntries);

            total += digits.Count(d => d.Length == 2);

            total += digits.Count(d => d.Length == 4);

            total += digits.Count(d => d.Length == 3);

            total += digits.Count(d => d.Length == 7);
        }

        return total.ToString();
    }
}