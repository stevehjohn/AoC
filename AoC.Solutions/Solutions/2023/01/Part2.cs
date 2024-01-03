using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._01;

[UsedImplicitly]
public class Part2 : Base
{
    private readonly Dictionary<string, string> _replacements = new()
    {
        { "one", "o1e" },
        { "two", "t2o" },
        { "three", "t3e" },
        { "four", "f4r" },
        { "five", "f5e" },
        { "six", "s6x" },
        { "seven", "s7n" },
        { "eight", "e8t" },
        { "nine", "n9e" },
    };
    
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
        foreach (var replacement in _replacements)
        {
            line = line.Replace(replacement.Key, replacement.Value);
        }
        
        return line;
    }
}