using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._21;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        // ReSharper disable once StringLiteralTypo
        var endState = Solve("abcdefgh", Input);

        return endState;
    }
}