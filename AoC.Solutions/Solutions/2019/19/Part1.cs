using AoC.Solutions.Solutions._2019.Computer;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._19;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var total = 0;

        for (var y = 0; y < 50; y++)
        {
            for (var x = 0; x < 50; x++)
            {
                // TODO: Add reboot function rather than new CPU every time.
                var cpu = new Cpu();

                cpu.Initialise(65536);

                cpu.LoadProgram(Input);

                cpu.UserInput.Enqueue(x);

                cpu.UserInput.Enqueue(y);

                cpu.Run();

                total += (int) cpu.UserOutput.Dequeue();
            }
        }

        return total.ToString();
    }
}