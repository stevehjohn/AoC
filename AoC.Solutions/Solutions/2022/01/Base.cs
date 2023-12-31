using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._01;

public abstract class Base : Solution
{
    public override string Description => "Calorie counting";

    protected List<int> Calories = new();

    protected void ParseInput()
    {
        var total = 0;

        foreach (var line in Input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                Calories.Add(total);

                total = 0;

                continue;
            }

            total += int.Parse(line);
        }
    }
}