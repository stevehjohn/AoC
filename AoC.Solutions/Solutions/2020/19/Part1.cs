using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._19;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly Dictionary<int, Rule> _rules = new();

    private readonly List<string> _messages = new();

    private Rule _rootRule;

    public override string GetAnswer()
    {
        ParseInput();

        var validCount = 0;

        foreach (var message in _messages)
        {
            validCount += _rootRule.IsValid(message) ? 1 : 0;
        }

        return validCount.ToString();
    }

    private void ParseInput()
    {
        var i = 0;

        while (!string.IsNullOrWhiteSpace(Input[i]))
        {
            var split = Input[i].Split(':', StringSplitOptions.TrimEntries);

            var ruleId = int.Parse(split[0]);

            var isLeaf = split[1][0] == '"';

            if (! _rules.TryGetValue(ruleId, out var rule))
            {
                rule = new Rule(ruleId);

                _rules.Add(rule.Id, rule);
            }

            if (isLeaf)
            {
                rule.Character = split[1][1];

                i++;

                continue;
            }

            var ruleString = split[1];

            if (ruleString.Contains('|'))
            {
                var orRules = ruleString.Split('|', StringSplitOptions.TrimEntries);

                rule.LeftRules = ParseRuleString(orRules[0]);

                rule.RightRules = ParseRuleString(orRules[1]);
            }
            else
            {
                rule.SubRules = ParseRuleString(ruleString);
            }

            i++;
        }

        _rootRule = _rules[0];

        i++;

        while (i < Input.Length)
        {
            _messages.Add(Input[i]);

            i++;
        }
    }

    private List<Rule> ParseRuleString(string rules)
    {
        var ids = rules.Split(' ', StringSplitOptions.TrimEntries).Select(int.Parse);

        var result = new List<Rule>();

        foreach (var id in ids)
        {
            if (_rules.ContainsKey(id))
            {
                result.Add(_rules[id]);
            }
            else
            {
                var newRule = new Rule(id);

                result.Add(newRule);

                _rules.Add(id, newRule);
            }
        }

        return result;
    }
}

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
        return Validate(input).Length == 0;
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

    // Hmm. If no matches, string will never decrease in length.
    // How to deal with that?
    private string ValidateCharacterRule(string input)
    {
        return input[0] == Character ? input[1..] : input;
    }

    private string ValidateOrRule(string input)
    {
        var inputCopy = input;

        foreach (var leftRule in LeftRules)
        {
            input = inputCopy;

            input = leftRule.Validate(input);

            foreach (var rightRule in RightRules)
            {
                input = rightRule.Validate(input);
            }
        }

        return input;
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