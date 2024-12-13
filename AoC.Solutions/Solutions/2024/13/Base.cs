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
}