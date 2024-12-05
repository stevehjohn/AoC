using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._05;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var result = 0;

        for (var i = 0; i < Updates.Count; i++)
        {
            var update = Updates[i];

            if (IsCorrect(update))
            {
                result += update[update.Count / 2];
            }
        }

        return result.ToString();
    }

    private bool IsCorrect(List<int> update)
    {
        var ordered = true;
        
        for (var i = 0; i < update.Count; i++)
        {
            if (i > 0)
            {
                if (Rules[update[i + 1]].Any(r => r == update[i]))
                {
                    return false;
                }
            }

            if (i < update.Count - 1)
            {
                if (Rules[update[i]].Any(r => r == update[i - 1]))
                {
                    return false;
                }
            }
        }

        return ordered;
    }
}