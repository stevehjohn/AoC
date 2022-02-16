using System.Text;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2015._10;

public abstract class Base : Solution
{
    public override string Description => "Elves look, elves say";

    protected static string ProcessRound(string input)
    {
        var result = new StringBuilder();

        var runLength = 1;

        var index = 0;

        while (index < input.Length - 1)
        {
            if (input[index] != input[index + 1])
            {
                result.Append(runLength);

                result.Append(input[index]);

                runLength = 1;
            }
            else
            {
                runLength++;
            }

            index++;
        }

        if (input[^1] == input[^2])
        {
            result.Append(runLength);

            result.Append(input[index]);
        }
        else
        {
            result.Append(1);

            result.Append(input[^1]);
        }

        return result.ToString();
    }
}