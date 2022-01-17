using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2020._04;

public abstract class Base : Solution
{
    public override string Description => "Passport validation";

    protected int CountValidPassports()
    {
        var validPassports = 0;

        var data = new Dictionary<string, string>();

        foreach (var line in Input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                validPassports += IsValidPassport(data) ? 1 : 0;

                data = new Dictionary<string, string>();

                continue;
            }

            var fields = line.Split(' ');

            foreach (var field in fields)
            {
                var components = field.Split(':');

                data.Add(components[0], components[1]);
            }
        }

        validPassports += IsValidPassport(data) ? 1 : 0;

        return validPassports;
    }

    protected abstract bool IsValidPassport(Dictionary<string, string> data);
}