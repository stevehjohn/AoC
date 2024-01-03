using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._05;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var nice = 0;

        foreach (var line in Input)
        {
            nice += IsNice(line) ? 1 : 0;
        }

        return nice.ToString();
    }

    private static bool IsNice(string line)
    {
        var nice = false;

        for (var i = 0; i < line.Length - 1; i++)
        {
            if (line.IndexOf($"{line[i]}{line[i + 1]}", i + 2, StringComparison.InvariantCultureIgnoreCase) > -1)
            {
                nice = true;

                break;
            }
        }

        if (! nice)
        {
            return false;
        }

        for (var i = 0; i < line.Length - 2; i++)
        {
            if (line[i] == line[i + 2])
            {
                return true;
            }
        }

        return false;
    }
}