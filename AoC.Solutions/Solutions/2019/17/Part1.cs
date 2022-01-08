using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._17;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        GetMap();

        var alignments = 0;

        for (var y = 1; y < Height - 1; y++)
        {
            for (var x = 1; x < Width - 1; x++)
            {
                if (Map[x, y] != '#')
                {
                    continue;
                }

                if (Map[x - 1, y] == '#' && Map[x, y - 1] == '#' && Map[x + 1, y] == '#' && Map[x, y + 1] == '#')
                {
                    alignments += x * y;
                }
            }
        }

        return alignments.ToString();
    }
}