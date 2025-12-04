using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._04;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var sum = 0;
        
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                var surrounding = 0;

                if (Map[y, x] == '.')
                {
                    continue;
                }

                for (var y1 = -1; y1 < 2; y1++)
                {
                    for (var x1 = -1; x1 < 2; x1++)
                    {
                        if (y1 == 0 && x1 == 0)
                        {
                            continue;
                        }

                        if (SafeCheckCell(y + y1, x + x1) == '@')
                        {
                            surrounding++;
                        }
                    }
                }

                if (surrounding < 4)
                {
                    sum++;
                }
            }
        }

        return sum.ToString();
    }
}