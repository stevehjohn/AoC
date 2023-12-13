using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._13;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        return GetAnswerInternal();
    }

    protected override bool IsReflectionPoint(List<string> lines, int start)
    {
        var top = start;

        var bottom = start + 1;

        var diff = 0;

        while (top >= 0 && bottom < lines.Count)
        {
            var topLine = lines[top];

            var bottomLine = lines[bottom];

            for (var x = 0; x < topLine.Length; x++)
            {
                if (topLine[x] != bottomLine[x])
                {
                    diff++;
                }
            }

            top--;

            bottom++;
        }

        return diff == 1;
    }
}