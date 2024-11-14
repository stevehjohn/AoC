using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._07;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var total = 0;

        foreach (var line in Input)
        {
            total += SupportsTls(line) ? 1 : 0;
        }

        return total.ToString();
    }

    private static bool SupportsTls(string input)
    {
        var parts = input.Split(['[', ']'], StringSplitOptions.TrimEntries);

        var containsPalindrome = false;

        for (var i = 0; i < parts.Length; i++)
        {
            if (i % 2 == 0)
            {
                containsPalindrome |= ContainsPalindrome(parts[i]);
            }
            else if (ContainsPalindrome(parts[i]))
            {
                return false;
            }
        }

        return containsPalindrome;
    }

    private static bool ContainsPalindrome(string data)
    {
        for (var i = 0; i < data.Length - 3; i++)
        {
            if (data[i] == data[i + 3] && data[i + 1] == data[i + 2] && data[i] != data[i + 1])
            {
                return true;
            }
        }

        return false;
    }
}