using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2020._15;

public abstract class Base : Solution
{
    public override string Description => "";

    public int GetAnswer(int turns)
    {
        var split = Input[0].Split(',').ToList();

        var numbers = split.Select(int.Parse).ToList();

        var whenSpoken = numbers.Select((n, i) => (Number: n, Index: i + 1)).ToDictionary(kvp => kvp.Number, kvp => new[] { 0, kvp.Index });

        var firstSpoken = numbers.Select((n, i) => (Number: n, Index: i + 1)).ToDictionary(kvp => kvp.Number, _ => true);

        var lastNumber = numbers.Last();

        var speak = 0;

        for (var t = numbers.Count + 1; t <= turns; t++)
        {
            speak = 0;

            if (firstSpoken[lastNumber])
            {
                firstSpoken[lastNumber] = false;
            }
            else
            {
                speak = whenSpoken[lastNumber][1] - whenSpoken[lastNumber][0];
            }

            if (whenSpoken.ContainsKey(speak))
            {
                whenSpoken[speak] = new [] { whenSpoken[speak][1], t };

                firstSpoken[speak] = false;
            }
            else
            {
                numbers.Add(speak);

                firstSpoken.Add(speak, true);

                whenSpoken.Add(speak, new[] { 0, t });
            }

            lastNumber = speak;
        }

        return speak;
    }
}