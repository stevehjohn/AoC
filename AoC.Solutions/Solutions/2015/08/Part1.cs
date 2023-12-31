using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._08;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var codeLength = 0;

        var memoryUsed = 0;

        foreach (var line in Input)
        {
            codeLength += line.Length;

            var data = line[1..^1];

            data = Regex.Unescape(data);

            memoryUsed += data.Length;
        }

        return (codeLength - memoryUsed).ToString();
    }
}