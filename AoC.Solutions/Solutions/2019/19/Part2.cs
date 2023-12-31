using AoC.Solutions.Solutions._2019.Computer;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._19;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var y = 100;

        var x = 0;

        while (true)
        {
            while (RunFor(x, y) == 0)
            {
                x++;
            }

            if (RunFor(x + 99, y - 99) == 1)
            {
                break;
            }

            y++;
        }

        return (x * 10_000 + (y - 99)).ToString();
    }

    private int RunFor(int x, int y)
    {
        // TODO: Add reboot function rather than new CPU every time.
        var cpu = new Cpu();

        cpu.Initialise(65536);

        cpu.LoadProgram(Input);

        cpu.UserInput.Enqueue(x);

        cpu.UserInput.Enqueue(y);

        cpu.Run();

        return (int) cpu.UserOutput.Dequeue();
    }
}