using AoC.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions._2019._05;

[UsedImplicitly]
public class Part1 : Solution
{
    public override string GetAnswer()
    {
        var cpu = new Computer.Cpu();

        cpu.Initialise();

        cpu.LoadProgram(Input);

        cpu.UserInput.Enqueue(1);

        cpu.Run();

        return cpu.UserOutput.Last().ToString();
    }
}