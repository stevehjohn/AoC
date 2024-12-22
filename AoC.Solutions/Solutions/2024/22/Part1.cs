using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._22;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var result = 0L;

        foreach (var line in Input)
        {
            result += SimulateBuyer(long.Parse(line));
            
            Console.WriteLine($"{line}: {SimulateBuyer(long.Parse(line))}");
        }

        return result.ToString();
    }
}