using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._04;

public abstract class Base : Solution
{
    public override string Description => "Scratchcards";

    protected int[] GetAllPoints()
    {
        var points = new int[Input.Length];

        for (var i = 0; i < Input.Length; i++)
        {
            points[i] = GetMatches(Input[i]);
        }

        return points;
    }

    private static int GetMatches(string line)
    {
        line = line.Split(':', StringSplitOptions.TrimEntries)[1];

        var parts = line.Split('|', StringSplitOptions.TrimEntries);

        var winningNumbers = parts[0].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

        var numbers = parts[1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

        var count = winningNumbers.Intersect(numbers).Count();

        return count;
    }
}