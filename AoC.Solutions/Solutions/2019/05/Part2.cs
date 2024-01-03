using AoC.Solutions.Solutions._2019.Computer;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._05;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var cpu = new Cpu();

        cpu.Initialise();

        cpu.LoadProgram(Input);

        cpu.UserInput.Enqueue(5);

        cpu.Run();

        return cpu.UserOutput.Last().ToString();
    }
}