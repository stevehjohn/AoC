using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._04;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var removed = 0;

        var toRemove = new bool[Map.Length];
        
        while (true)
        {
            var anyToRemove = false;

            for (var index = 0; index < Map.Length; index++)
            {
                if (Map[index] == '.')
                {
                    continue;
                }

                if (Map.CountAdjacentCells(index, '@') < 4)
                {
                    toRemove[index] = true;

                    anyToRemove = true;
                }
            }

            if (! anyToRemove)
            {
                break;
            }

            for (var i = 0; i < Map.Length; i++)
            {
                if (toRemove[i])
                {
                    Map[i] = '.';
                    
                    toRemove[i] = false;

                    removed++;
                }
            }
        }

        return removed.ToString();
    }
}