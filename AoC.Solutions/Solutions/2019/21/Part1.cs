using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._21;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        // TODO: Calculate programatically
        return RunDroid("NOT B J\nNOT C T\nOR T J\nAND D J\nNOT A T\nOR T J\nWALK\n");
    }
}