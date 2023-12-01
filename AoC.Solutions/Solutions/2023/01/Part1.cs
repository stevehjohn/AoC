using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._01;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var sum = 0;
        
        foreach (var line in Input)
        {
            sum += GetNumber(line);
        }

        return sum.ToString();
    }

    private int GetNumber(string line)
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