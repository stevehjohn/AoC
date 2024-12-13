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
        var targetX = (long) machine.Target.X;

        var targetY = (long) machine.Target.Y;

        if (isPart2)
        {
            targetX += 10_000_000_000_000;

            targetY += 10_000_000_000_000;
        }
        
        var originalDeterminant = machine.ButtonA.X * machine.ButtonB.Y - machine.ButtonA.Y * machine.ButtonB.X;
        
        var determinantX = targetX * machine.ButtonB.Y - targetY * machine.ButtonB.X;
        
        var determinantY = machine.ButtonA.X * targetY - machine.ButtonA.Y * targetX;
 
        if (determinantX % originalDeterminant != 0 || determinantY % originalDeterminant != 0)
        {
            return (0, 0);
        }
 
        var x = determinantX / originalDeterminant;
        
        var y = determinantY / originalDeterminant;
 
        if (! isPart2 && (x > 100 || y > 100))
        {
            return (0, 0);
        }

        return (x, y);
    }
}