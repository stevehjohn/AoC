using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2020._19;

public abstract class Base : Solution
{
    public override string Description => "Message pattern matching";

    protected readonly Dictionary<int, Rule> Rules = new();

    protected readonly List<string> Messages = [];

    protected Rule RootRule;

    protected void ParseInput(bool replaceRules = false)
    {
        var i = 0;

        while (!string.IsNullOrWhiteSpace(Input[i]))
        {
            var split = Input[i].Split(':', StringSplitOptions.TrimEntries);

            var ruleId = int.Parse(split[0]);

            if (replaceRules)
            {
                if (ruleId == 8)
                {
                    split[1] = "42 | 42 8";
                }

                if (ruleId == 11)
                {
                    split[1] = "42 31 | 42 11 31";
                }
            }

            var isLeaf = split[1][0] == '"';

            if (! Rules.TryGetValue(ruleId, out var rule))
            {
                rule = new Rule(ruleId);

                Rules.Add(rule.Id, rule);
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

        RootRule = Rules[0];

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
            if (Rules.TryGetValue(id, out var rule))
            {
                result.Add(rule);
            }
            else
            {
                var newRule = new Rule(id);

                result.Add(newRule);

                Rules.Add(id, newRule);
            }
        }

        return result;
    }
}