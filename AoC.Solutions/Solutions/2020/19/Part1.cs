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

        //foreach (var message in _messages)
        //{
        //    validCount += _rootRule.IsValid(message) ? 1 : 0;
        //}

        validCount = _rootRule.IsValid("aaabbb") ? 1 : 0;

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