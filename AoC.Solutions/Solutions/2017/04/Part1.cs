using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._04;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var valid = 0;

        foreach (var line in Input)
        {
            valid += IsValidPassphrase(line) ? 1 : 0;
        }

        return valid.ToString();
    }

    private static bool IsValidPassphrase(string line)
    {
        var encountered = new HashSet<string>();

        var words = line.Split(' ');

        foreach (var word in words)
        {
            if (! encountered.Add(word))
            {
                return false;
            }
        }

        return true;
    }
}