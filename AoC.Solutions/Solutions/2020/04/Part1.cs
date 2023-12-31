using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._04;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var validPassports = CountValidPassports();
        
        return validPassports.ToString();
    }

    protected override bool IsValidPassport(Dictionary<string, string> data)
    {
        var requiredFields = new [] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };

        return data.Select(kvp => kvp.Key).Intersect(requiredFields).Count() > 6;
    }
}