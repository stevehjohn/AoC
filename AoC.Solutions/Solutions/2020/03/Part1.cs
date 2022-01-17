using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._03;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var trees = 0;

        var x = 0;

        for (var y = 1; y < Height; y++)
        {
            x += 3;

            if (x >= Width)
            {
                x -= Width;
            }

            trees += Map[x, y] ? 1 : 0;
        }

        return trees.ToString();
    }
}