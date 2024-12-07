using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._07;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var result = 0L;

        foreach (var line in Input)
        {
            var total = ProcessLineSimple(line);

            if (total > 0)
            {
                result += total;
            }
        }
        
        return result.ToString();
    }
}