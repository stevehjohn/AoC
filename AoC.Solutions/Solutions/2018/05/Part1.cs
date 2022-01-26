using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._05;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        return ReactPolymer(Input[0]).ToString();
    }
}