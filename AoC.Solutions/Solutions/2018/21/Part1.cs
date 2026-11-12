using AoC.Solutions.Extensions;
using AoC.Solutions.Solutions._2018.TimeMachine;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._21;

[UsedImplicitly]
public class Part1 : Base
{
    // ReSharper disable StringLiteralTypo
    public override string GetAnswer()
    {
        var cpu = new Cpu(6);

        cpu.LoadProgram(Input);

        cpu.Run(-1, OpCode.Eqrr);

        var breakOn = Input.Single(i => i.StartsWith("eqrr"));

        var register = int.FastParse(breakOn.Split(' ', StringSplitOptions.TrimEntries)[1]);
        
        return cpu.GetRegisters()[register].ToString();
    }
}