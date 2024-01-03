using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._21;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        return RunDroid("NOT B J\nNOT C T\nOR T J\nAND D J\nAND H J\nNOT A T\nOR T J\nRUN\n");
    }
}