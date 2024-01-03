using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._02;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var valid = 0;

        foreach (var line in Input)
        {
            var (min, max, character, password) = ParseLine(line);

            var occurrences = password.Count(c => c == character);

            if (occurrences >= min && occurrences <= max)
            {
                valid++;
            }
        }

        return valid.ToString();
    }
}