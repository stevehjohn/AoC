using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._08;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var total = 0;

        foreach (var line in Input)
        {
            var split = line.Split('|', StringSplitOptions.TrimEntries);

            var digit = new Digit(split[0].Split(' ', StringSplitOptions.TrimEntries), split[1].Split(' ', StringSplitOptions.TrimEntries));

            total += digit.GetValue();
        }

        return total.ToString();
    }
}