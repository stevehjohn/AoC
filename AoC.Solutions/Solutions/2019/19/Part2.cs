using AoC.Solutions.Solutions._2019.Computer;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._19;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        /*
         * #.............
         * ..............
         * ..............
         * .#............
         * ..............
         * .##...........
         * .##...........
         * ..##..........
         * ..###.........
         */

        var y = 0;

        var xMin = 0;

        var xMax = -1;

        while (true)
        {
            while (RunFor(xMin, y) == 0)
            {
                xMin++;
            }

            if (xMax == -1)
            {
                xMax = xMin;
            }

            while (RunFor(xMax, y) == 1)
            {
                xMax++;
            }

            y++;
        }

        return "TESTING";
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