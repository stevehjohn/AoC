using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._19;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly Dictionary<int, Rule> _rules = new();

    private readonly List<string> _messages = new();

    public override string GetAnswer()
    {
        ParseInput();

        return "TESTING";
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
                result.Add(new Rule(id));
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