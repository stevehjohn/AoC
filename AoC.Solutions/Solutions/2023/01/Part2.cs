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
        line = ParseFirstNumberString(line);

        line = ParseLastNumberString(line);

        return line;
    }

    private string ParseFirstNumberString(string line)
    {
        var first = int.MaxValue;

        var firstNumber = 0;
        
        for (var i = 0; i < 9; i++)
        {
            var index = line.IndexOf(_numbers[i], StringComparison.InvariantCultureIgnoreCase);

            if (index >= 0 && index < first)
            {
                first = index;

                firstNumber = i;
            }
        }

        if (first != int.MaxValue)
        {
            line = line.Replace(_numbers[firstNumber], (firstNumber + 1).ToString());
        }

        return line;
    }

    private string ParseLastNumberString(string line)
    {
        var last = int.MaxValue;

        var lastNumber = 0;
        
        for (var i = 0; i < 9; i++)
        {
            var index = line.LastIndexOf(_numbers[i], StringComparison.InvariantCultureIgnoreCase);

            if (index >= 0 && index < last)
            {
                last = index;

                lastNumber = i;
            }
        }

        if (last != int.MaxValue)
        {
            line = line.Replace(_numbers[lastNumber], (lastNumber + 1).ToString());
        }

        return line;
    }
}