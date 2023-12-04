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
        var winningNumbersString = line[10..39];

        var numbersString = line[42..];
        
        var winningNumbers = winningNumbersString.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

        var numbers = numbersString.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

        var count = winningNumbers.Intersect(numbers).Count();

        return count;
    }
}