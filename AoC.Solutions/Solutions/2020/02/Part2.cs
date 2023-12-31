using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._02;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var valid = 0;

        foreach (var line in Input)
        {
            var (first, second, character, password) = ParseLine(line);

            if (password[first - 1] == password[second - 1])
            {
                continue;
            }

            if (password[first - 1] == character || password[second - 1] == character)
            {
                valid++;
            }
        }

        return valid.ToString();
    }
}