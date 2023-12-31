using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._03;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var trees = CountTrees(3, 1);

        return trees.ToString();
    }
}