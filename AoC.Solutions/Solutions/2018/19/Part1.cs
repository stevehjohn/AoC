using AoC.Solutions.Solutions._2018.TimeMachine;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._19;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var cpu = new Cpu(6);

        cpu.LoadProgram(Input);

        cpu.Run();

        return cpu.GetRegisters()[0].ToString();
    }
}