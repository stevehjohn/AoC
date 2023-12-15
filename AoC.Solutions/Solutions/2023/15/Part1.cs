using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._15;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var steps = ParseInput();

        var sum = 0L;

        foreach (var step in steps)
        {
            sum += Hash(step);
        }
        
        return sum.ToString();
    }

    private static int Hash(string input)
    {
        var hash = 0;
        
        foreach (var c in input)
        {
            hash += (byte) c;

            hash *= 17;

            hash %= 256;
        }

        return hash;
    }

    private List<string> ParseInput()
    {
        var parts = Input[0].Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        return parts.ToList();
    }
}