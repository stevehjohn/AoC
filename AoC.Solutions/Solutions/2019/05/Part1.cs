using AoC.Solutions.Infrastructure;
using AoC.Solutions.Solutions._2019.Computer;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._05;

[UsedImplicitly]
public class Part1 : Solution
{
    public override string Description => "Air conditioning (CPU extension 1)";

    public override string GetAnswer()
    {
        var cpu = new Cpu();

        cpu.Initialise();

        cpu.LoadProgram(Input);

        cpu.UserInput.Enqueue(1);

        cpu.Run();

        return cpu.UserOutput.Last().ToString();
    }
}