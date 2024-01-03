using System.Text;

namespace AoC.Solutions.Solutions._2018._12;

public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var previousState = string.Empty;

        var generation = 0;

        while (true)
        {
            RunGeneration();

            generation++;

            var state = PotsToString();

            if (state == previousState)
            {
                break;
            }

            previousState = state;
        }

        return PotsWithPlants.Select(p => p + 50_000_000_000 - generation).Sum().ToString();
    }

    private string PotsToString()
    {
        var builder = new StringBuilder();

        for (var i = PotsWithPlants.Min() - 2; i < PotsWithPlants.Max() + 3; i++)
        {
            builder.Append(PotsWithPlants.Contains(i) ? '#' : '.');
        }

        return builder.ToString();
    }
}