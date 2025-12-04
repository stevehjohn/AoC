using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._04;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var removed = 0;

        var toRemove = new HashSet<(int X, int Y)>();

        while (true)
        {
            Map.ForAllCells(centre =>
            {
                if (centre.Value == '.')
                {
                    return;
                }

                var surrounding = 0;

                Map.ForAdjacentCells(centre.X, centre.Y,
                    cell =>
                    {
                        if (cell == '@')
                        {
                            surrounding++;
                        }
                    });

                if (surrounding < 4)
                {
                    toRemove.Add((centre.X, centre.Y));
                }
            });

            if (toRemove.Count == 0)
            {
                break;
            }

            removed += toRemove.Count;

            foreach (var item in toRemove)
            {
                Map[item.X, item.Y] = '.';
            }
            
            toRemove.Clear();
        }

        return removed.ToString();
    }
}