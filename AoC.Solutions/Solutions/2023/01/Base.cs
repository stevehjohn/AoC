using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._01;

public abstract class Base : Solution
{
    public override string Description => "Trebuchet?!";

    protected int GetNumber(string line)
    {
        var first = ' ';
        
        foreach (var letter in line)
        {
            if (char.IsNumber(letter))
            {
                first = letter;

                break;
            }
        }

        line = new string(line.Reverse().ToArray());

        var last = ' ';
        
        foreach (var letter in line)
        {
            if (char.IsNumber(letter))
            {
                last = letter;

                break;
            }
        }

        return int.Parse($"{first}{last}");
    }
}