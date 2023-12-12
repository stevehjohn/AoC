using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._12;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var sum = 0L;

        foreach (var line in Input)
        {
            var data = ParseLine(UnfoldLine(line));

            sum += GetArrangements(data.Row, data.Groups);
        }
        
        return sum.ToString();
    }

    private static string UnfoldLine(string line)
    {
        var parts = line.Split(' ');

        return $"{parts[0]}?{parts[0]}?{parts[0]}?{parts[0]}?{parts[0]} {parts[1]},{parts[1]},{parts[1]},{parts[1]},{parts[1]}";
    }
}