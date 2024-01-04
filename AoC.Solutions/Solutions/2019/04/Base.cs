using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2019._04;

public abstract class Base : Solution
{
    public override string Description => "Forgotten password";

    protected string GetAnswer(bool singlePairRequired)
    {
        var range = Input[0].Split('-', StringSplitOptions.TrimEntries).Select(int.Parse).ToArray();

        var validCount = 0;

        for (var i = range[0]; i <= range[1]; i++)
        {
            if (CheckPasswordValidity(i.ToString(), singlePairRequired))
            {
                validCount++;
            }
        }

        return validCount.ToString();
    }

    private static bool CheckPasswordValidity(string password, bool singlePairRequired)
    {
        var adjacent = new Dictionary<char, int>
                       {
                           { '0', 0 },
                           { '1', 0 },
                           { '2', 0 },
                           { '3', 0 },
                           { '4', 0 },
                           { '5', 0 },
                           { '6', 0 },
                           { '7', 0 },
                           { '8', 0 },
                           { '9', 0 }
                       };

        for (var x = 0; x < 5; x++)
        {
            if (password[x + 1] < password[x])
            {
                return false;
            }

            if (password[x + 1] == password[x])
            {
                adjacent[password[x]]++;
            }
        }

        return singlePairRequired
            ? adjacent.ContainsValue(1)
            : adjacent.Any(p => p.Value > 0);
    }
}