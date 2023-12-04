using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._04;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var points = 0;

        foreach (var line in Input)
        {
            points += GetPoints(line);
        }
        
        return points.ToString();
    }

    private int GetPoints(string line)
    {
        line = line.Split(':', StringSplitOptions.TrimEntries)[1];

        var parts = line.Split('|', StringSplitOptions.TrimEntries);

        var winningNumbers = parts[0].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

        var numbers = parts[1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

        var count = winningNumbers.Intersect(numbers).Count();

        if (count == 0)
        {
            return 0;
        }

        var points = 1;

        for (var i = 1; i < count; i++)
        {
            points *= 2;
        }

        return points;
    }
}