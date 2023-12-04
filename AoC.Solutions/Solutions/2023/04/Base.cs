using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._04;

public abstract class Base : Solution
{
    public override string Description => "Scratchcards";

    private int[] _winningNumbers = new int[10];

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
                : (winningNumbersString[location] - '0') * 10 + winningNumbersString[location + 1] - '0';
        }

        var count = 0;
        
        for (var i = 0; i < 25; i++)
        {
            var location = i * 3;

            var number = numbersString[location] == ' '
                ? numbersString[location + 1] - '0'
                : (numbersString[location] - '0') * 10 + numbersString[location + 1] - '0';

            for (var j = 0; j < 10; j++)
            {
                if (_winningNumbers[j] == number)
                {
                    count++;

                    break;
                }
            }
        }

        return count;
    }
}