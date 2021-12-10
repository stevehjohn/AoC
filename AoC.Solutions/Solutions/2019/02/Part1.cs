using AoC.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions._2019._02;

[UsedImplicitly]
public class Part1 : Solution
{
    public override string GetAnswer()
    {
        var cpu = new Computer.Cpu();

        cpu.Initialise();

        cpu.LoadProgram(Input);

        cpu.Memory[1] = 12;
        
        cpu.Memory[2] = 2;

        cpu.Run();

        return cpu.Memory[0].ToString();
    }
}