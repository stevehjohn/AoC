using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._04;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var requiredFields = new [] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };

        var validPassports = 0;

        var matched = 0;

        foreach (var line in Input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                if (matched > 6)
                {
                    validPassports++;
                }

                matched = 0;

                continue;
            }

            var fields = line.Split(' ');

            foreach (var field in fields)
            {
                if (requiredFields.Contains(field[..3]))
                {
                    matched++;
                }
            }
        }

        if (matched > 6)
        {
            validPassports++;
        }
        
        return validPassports.ToString();
    }
}