using AoC.Solutions.Solutions._2016.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._12;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var result = Cpu.RunProgram(Input, new Dictionary<char, int>());

        return result.ToString();
    }
}