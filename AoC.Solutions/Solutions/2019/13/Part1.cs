using AoC.Solutions.Solutions._2019.Computer;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._13;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var cpu = new Cpu();

        cpu.Initialise(65536);

        cpu.LoadProgram(Input);

        cpu.Run();

        var position = 0;

        var blocks = 0L;

        while (cpu.UserOutput.Count > 0)
        {
            var output = cpu.UserOutput.Dequeue();

            if ((position + 1) % 3 == 0 && output == 2)
            {
                blocks++;
            }

            position++;
        }

        return blocks.ToString();
    }
}