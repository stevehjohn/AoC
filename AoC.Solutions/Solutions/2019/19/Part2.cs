using AoC.Solutions.Common;
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

        // Starting at 100 as signal is intermittent to begin with (see image above).
        var y = 100;

        var xMin = 0;

        var xMax = -1;

        var xMaxes = new FixedLengthQueue<int>(100);

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

            xMaxes.Add(xMax);

            if (xMaxes.First() - xMin > 100)
            {
                break;
            }

            y++;
        }

        return (xMin * 10_000 + (y - 100)).ToString();
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