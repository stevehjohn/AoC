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
            Map.ForAllCells((x, y, centre) =>
            {
                if (centre == '.')
                {
                    return;
                }

                var surrounding = 0;

                Map.ForAdjacentCells(x, y,
                    cell =>
                    {
                        if (cell == '@')
                        {
                            surrounding++;
                        }
                    });

                if (surrounding < 4)
                {
                    toRemove.Add((x, y));
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