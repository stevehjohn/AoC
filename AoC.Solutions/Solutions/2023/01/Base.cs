using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._01;

public abstract class Base : Solution
{
    public override string Description => "Trebuchet";

    protected static int GetNumber(string line)
    {
        var first = ' ';
        
        var last = ' ';

        for (var i = 0; i < line.Length; i++)
        {
            if (char.IsNumber(line[i]) && first == ' ')
            {
                first = line[i];
            }

            if (char.IsNumber(line[line.Length - i - 1]) & last == ' ')
            {
                last = line[line.Length - i - 1];
            }

            if (first != ' ' && last != ' ')
            {
                break;
            }
        }

        return int.Parse($"{first}{last}");
    }
}