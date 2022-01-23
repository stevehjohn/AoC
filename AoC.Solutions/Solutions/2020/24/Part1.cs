using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._24;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        FlipTiles();

        return BlackTiles.Count.ToString();
    }
}