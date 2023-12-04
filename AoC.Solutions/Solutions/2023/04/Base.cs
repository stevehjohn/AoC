using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._04;

public abstract class Base : Solution
{
    public override string Description => "Scratchcards";

    private int[] _winningNumbers = new int[10];

    private int[] _numbers = new int[25];

    protected int[] GetAllPoints()
    {
        var points = new int[Input.Length];

        for (var i = 0; i < Input.Length; i++)
        {
            points[i] = GetMatches(Input[i]);
        }

        return points;
    }

    private int GetMatches(string line)
    {
        var winningNumbersString = line[10..39];

        var numbersString = line[42..];

        for (var i = 0; i < 10; i++)
        {
            var location = i * 3;

            _winningNumbers[i] = winningNumbersString[location] == ' '
                ? winningNumbersString[location + 1] - '0'
                : int.Parse(winningNumbersString[location..(location + 2)]);
        }

        for (var i = 0; i < 25; i++)
        {
            var location = i * 3;

            _numbers[i] = numbersString[location] == ' '
                ? numbersString[location + 1] - '0'
                : int.Parse(numbersString[location..(location + 2)]);
        }

        var count = _winningNumbers.Intersect(_numbers).Count();

        return count;
    }
}