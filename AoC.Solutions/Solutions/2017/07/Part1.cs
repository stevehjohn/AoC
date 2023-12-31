using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._07;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        return RootNode.Name;
    }
}