using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._05;

[UsedImplicitly]
public class Part1 : Base
{
    private static readonly string[] SourceArray = ["ab", "cd", "pq", "xy"];

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
        if (SourceArray.Any(line.Contains))
        {
            return false;
        }

        var nice = false;

        for (var i = 0; i < line.Length - 1; i++)
        {
            if (line[i] == line[i + 1])
            {
                nice = true;

                break;
            }
        }

        if (! nice)
        {
            return false;
        }

        return line.Count(c => c is 'a' or 'e' or 'i' or 'o' or 'u') > 2;
    }
}