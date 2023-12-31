using AoC.Solutions.Solutions._2016.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._23;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        Input[4] = "mul b d a";
        Input[5] = "jnz 0 0";
        Input[6] = "jnz 0 0";
        Input[7] = "jnz 0 0";
        Input[8] = "cpy 0 c";
        Input[9] = "cpy 0 d";

        var result = Cpu.RunProgram(Input, new Dictionary<char, int> { { 'a', 12 } });

        return result.ToString();
    }
}