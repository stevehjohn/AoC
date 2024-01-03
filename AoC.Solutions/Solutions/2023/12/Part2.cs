using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._12;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var sum = 0L;

        var lines = Input.OrderBy(l => l.Length).Select(l => ParseLine(UnfoldLine(l))).ToArray();
        
        foreach (var line in lines)
        {
            //var data = ParseLine(UnfoldLine(line));

            sum += GetArrangements(line.Row, line.Groups, line.Sum);
        }
        
        return sum.ToString();
    }

    private static string UnfoldLine(string line)
    {
        var parts = line.Split(' ');

        return $"{parts[0]}?{parts[0]}?{parts[0]}?{parts[0]}?{parts[0]} {parts[1]},{parts[1]},{parts[1]},{parts[1]},{parts[1]}";
    }
}