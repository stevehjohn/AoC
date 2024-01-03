using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._03;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var trees = (long) CountTrees(1, 1) * CountTrees(3, 1) * CountTrees(5, 1) * CountTrees(7, 1) * CountTrees(1, 2);

        return trees.ToString();
    }
}