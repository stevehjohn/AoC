using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._01;

[UsedImplicitly]
public class Part2 : Base
{
    private readonly string[] _numbers = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
    
    public override string GetAnswer()
    {
        var sum = 0;
        
        foreach (var line in Input)
        {
            sum += GetNumber(ParseNumbers(line));
        }

        return sum.ToString();
    }

    private string ParseNumbers(string line)
    {
        var index = 1;

        foreach (var number in _numbers)
        {
            line = line.Replace(number, index.ToString());

            index++;
        }

        return line;
    }
}