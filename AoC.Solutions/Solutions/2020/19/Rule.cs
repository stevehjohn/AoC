namespace AoC.Solutions.Solutions._2020._19;

public class Rule
{
    public int Id { get; }

    public char Character { get; set; }

    public List<Rule> SubRules { get; set; }

    public List<Rule> LeftRules { get; set; }

    public List<Rule> RightRules { get; set; }

    public Rule(int id)
    {
        Id = id;
    }

    public bool IsValid(string input)
    {
        var result = Validate(input);

        return result.IsValid && result.Remaining.Length == 0;
    }

    public (bool IsValid, string Remaining) Validate(string input)
    {
        if (input.Length == 0)
        {
            return (true, string.Empty);
        }

        if (SubRules != null)
        {
            return ValidateSubRules(input);
        }

        if (LeftRules != null && RightRules != null)
        {
            return ValidateOrRule(input);
        }

        return ValidateCharacterRule(input);
    }

    private (bool IsValid, string Remaining) ValidateCharacterRule(string input)
    {
        return (input[0] == Character, input[0] == Character ? input[1..] : input);
    }

    private (bool IsValid, string Remaining) ValidateOrRule(string input)
    {
        var result = LeftRules[0].Validate(input);

        if (result.IsValid)
        {
            if (LeftRules.Count == 1)
            {
                return (true, result.Remaining);
            }

            result = LeftRules[1].Validate(result.Remaining);

            if (result.IsValid)
            {
                return (true, result.Remaining);
            }
        }

        result = RightRules[0].Validate(input);

        if (result.IsValid)
        {
            if (RightRules.Count == 1)
            {
                return (true, result.Remaining);
            }

            result = RightRules[1].Validate(result.Remaining);

            if (RightRules.Count == 2)
            {
                if (result.IsValid)
                {
                    return (true, result.Remaining);
                }

                return (false, input);
            }

            result = RightRules[2].Validate(result.Remaining);

            if (result.IsValid)
            {
                return (true, result.Remaining);
            }
        }

        return (false, input);
    }

    private (bool IsValid, string Remaining) ValidateSubRules(string input)
    {
        (bool IsValid, string Remaining) result = (false, input);

        foreach (var rule in SubRules)
        {
            result = rule.Validate(input);

            if (! result.IsValid)
            {
                return result;
            }

            input = result.Remaining;
        }

        return result;
    }

    public override string ToString()
    {
        if (Character != '\0')
        {
            return $"{Id}: \"{Character}\"";
        }

        if (SubRules != null)
        {
            return $"{Id}: {string.Join(", ", SubRules.Select(r => r.Id))}";
        }

        return $"{Id}: {string.Join(", ", LeftRules.Select(r => r.Id))} | {string.Join(", ", RightRules.Select(r => r.Id))}";
    }
}