using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._04;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var valid = 0;

        foreach (var line in Input)
        {
            if (IsValidPassphrase(line))
            {
                valid++;
            }
        }

        return valid.ToString();
    }

    private static bool IsValidPassphrase(string line)
    {
        var encountered = new HashSet<string>();

        foreach (var word in line.Split(' '))
        {
            var chars = word.ToCharArray();

            Array.Sort(chars);

            if (!encountered.Add(new string(chars)))
            {
                return false;
            }
        }

        return true;
    }
}