using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2020._15;

public abstract class Base : Solution
{
    public override string Description => "Number memory game";

    public int GetAnswer(int turns)
    {
        var split = Input[0].Split(',').ToList();

        var numbers = split.Select(int.Parse).ToList();

        var lastSpoken = new int[turns];

        var penultimateSpoken = new int[turns];

        for (var i = 1; i <= numbers.Count; i++)
        {
            lastSpoken[numbers[i - 1]] = i;
        }

        var lastNumber = numbers.Last();

        var speak = 0;

        for (var t = numbers.Count + 1; t <= turns; t++)
        {
            speak = 0;

            if (penultimateSpoken[lastNumber] != 0)
            {
                speak = lastSpoken[lastNumber] - penultimateSpoken[lastNumber];
            }

            penultimateSpoken[speak] = lastSpoken[speak];

            lastSpoken[speak] = t;

            lastNumber = speak;
        }

        return speak;
    }
}