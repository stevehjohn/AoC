using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._04;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var validPassports = CountValidPassports();

        return validPassports.ToString();
    }

    protected override bool IsValidPassport(Dictionary<string, string> data)
    {
        var requiredFields = new[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };

        if (data.Select(kvp => kvp.Key).Intersect(requiredFields).Count() < 7)
        {
            return false;
        }

        if (! ValidateYear(data["byr"], 1920, 2002))
        {
            return false;
        }

        if (! ValidateYear(data["iyr"], 2010, 2020))
        {
            return false;
        }

        if (! ValidateYear(data["eyr"], 2020, 2030))
        {
            return false;
        }

        if (! ValidateHeight(data["hgt"]))
        {
            return false;
        }

        if (! ValidateHairColour(data["hcl"]))
        {
            return false;
        }

        if (! ValidateEyeColour(data["ecl"]))
        {
            return false;
        }

        if (! ValidatePassportId(data["pid"]))
        {
            return false;
        }

        return true;
    }

    private static bool ValidateYear(string value, int min, int max)
    {
        if (! int.TryParse(value, out var year))
        {
            return false;
        }

        return year >= min && year <= max;
    }

    private static bool ValidateHeight(string value)
    {
        if (! int.TryParse(value.AsSpan(0, value.Length - 2), out var height))
        {
            return false;
        }

        if (value.EndsWith("cm"))
        {
            return height is >= 150 and <= 193;
        }

        return height is >= 59 and <= 76;
    }

    private static bool ValidateHairColour(string value)
    {
        if (value.Length != 7 || value[0] != '#')
        {
            return false;
        }

        foreach (var c in value[1..])
        {
            if (! char.IsNumber(c) && c is < 'a' or > 'f')
            {
                return false;
            }
        }

        return true;
    }

    private static bool ValidateEyeColour(string value)
    {
        return new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }.Any(c => c == value);
    }

    private static bool ValidatePassportId(string value)
    {
        if (! int.TryParse(value, out _))
        {
            return false;
        }

        return value.Length == 9;
    }
}