using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._13;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        return GetAnswerInternal();
    }
    
    protected override bool IsReflectionPoint(List<string> lines, int start)
    {
        var top = start;

        var bottom = start + 1;

        while (top >= 0 && bottom < lines.Count)
        {
            if (! lines[top].Equals(lines[bottom]))
            {
                return false;
            }

            top--;

            bottom++;
        }

        return true;
    }
}