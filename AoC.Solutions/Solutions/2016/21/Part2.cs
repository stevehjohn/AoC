using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._21;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        // ReSharper disable once StringLiteralTypo
        var endState = Solve("fbgdceah", Input.Reverse().ToArray(), true);

        return endState;
    }
}