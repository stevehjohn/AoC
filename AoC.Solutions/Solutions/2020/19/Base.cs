using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2020._19;

public abstract class Base : Solution
{
    public override string Description => "Message pattern matching";

    private readonly Dictionary<int, Rule> _rules = new();

    protected readonly List<string> Messages = new();

    protected Rule RootRule;

    protected void ParseInput()
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

        RootRule = _rules[0];

        i++;

        while (i < Input.Length)
        {
            Messages.Add(Input[i]);

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