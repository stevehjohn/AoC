using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._04;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var removed = 0;

        var toRemove = new HashSet<int>();

        while (true)
        {
            for (var index = 0; index < Map.Length; index++)
            {
                if (Map[index] == '.')
                {
                    continue;
                }

                var surrounding = 0;

                Map.ForAdjacentCells(index, cell =>
                    {
                        if (cell == '@')
                        {
                            surrounding++;
                        }
                    });

                if (surrounding < 4)
                {
                    toRemove.Add(index);
                }
            }

            if (toRemove.Count == 0)
            {
                break;
            }

            removed += toRemove.Count;

            foreach (var cell in toRemove)
            {
                Map[cell] = '.';
            }
            
            toRemove.Clear();
        }

        return removed.ToString();
    }
}