namespace AoC.Solutions.Solutions._2020._19;

public class Rule
{
    public int Id { get; }

    public char Character { get; set;  }

    public List<Rule> SubRules { get; set; }

    public List<Rule> LeftRules { get; set; }

    public List<Rule> RightRules { get; set; }

    public Rule(int id)
    {
        Id = id;
    }

    public bool IsValid(string input)
    { 
        input = Validate(input);

        return input.Length == 0;
    }

    public string Validate(string input)
    {
        if (input.Length == 0)
        {
            return input;
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

    private string ValidateCharacterRule(string input)
    {
        return input[0] == Character ? input[1..] : input;
    }

    // Hmm. If no matches, string will never decrease in length. Maybe.
    // How to deal with that?
    private string ValidateOrRule(string input)
    {
        var inputCopy1 = input;

        foreach (var leftRule in LeftRules)
        {
            input = inputCopy1;

            input = leftRule.Validate(input);

            if (input.Length < inputCopy1.Length)
            {
                var inputCopy2 = input;

                foreach (var rightRule in RightRules)
                {
                    input = rightRule.Validate(inputCopy2);

                    if (input.Length < inputCopy2.Length)
                    {
                        return input;
                    }
                }
            }
        }

        return inputCopy1;
    }

    private string ValidateSubRules(string input)
    {
        foreach (var rule in SubRules)
        {
            input = rule.Validate(input);
        }

        return input;
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