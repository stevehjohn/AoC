using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._01;

public abstract class Base : Solution
{
    public override string Description => "Trebuchet";

    protected static int GetNumber(string line)
    {
        var first = -1;
        
        var last = -1;

        for (var i = 0; i < line.Length; i++)
        {
            if (char.IsNumber(line[i]) && first == -1)
            {
                first = (line[i] - '0') * 10;
            }

            if (char.IsNumber(line[line.Length - i - 1]) & last == -1)
            {
                last = line[line.Length - i - 1] - '0';
            }

            if (first != -1 && last != -1)
            {
                break;
            }
        }

        return first + last;
    }
}