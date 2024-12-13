using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._13;

public abstract class Base : Solution
{
    public override string Description => "Claw contraption";

    protected Machine ParseMachine(int index)
    {
        var startLine = index * 4;

        if (startLine >= Input.Length)
        {
            return null;
        }

        var a = ParseLine(Input[startLine]);
        
        var b = ParseLine(Input[startLine + 1]);
        
        var c = ParseLine(Input[startLine + 2]);

        return new Machine(a, b, c);
    }

    private static Point ParseLine(string line)
    {
        line = line[(line.IndexOf(':') + 2)..];

        var parts = line.Split(", ", StringSplitOptions.TrimEntries);

        return new Point(int.Parse(parts[0][2..]), int.Parse(parts[1][2..]));
    }

    protected static (long A, long B) GetButtonPresses(Machine machine, bool isPart2 = false)
    {
        var targetX = (double) machine.Target.X;

        var targetY = (double) machine.Target.Y;

        if (isPart2)
        {
            targetX *= 10_000_000_000_000;

            targetY *= 10_000_000_000_000;
        }

        for (var a = 1; a < 100; a++)
        {
            for (var b = 1; b < 100; b++)
            {
                if (targetX % (machine.ButtonA.X * a + machine.ButtonB.X * b) == 0)
                {
                    if (targetY % (machine.ButtonA.Y * a + machine.ButtonB.Y * b) == 0)
                    {
                        var pressesBase = (long) (targetX / (machine.ButtonA.X * a + machine.ButtonB.X * b));
                        
                        return (a * pressesBase, b * pressesBase);
                    }
                }
            }
        }

        return (0, 0);
    }
}