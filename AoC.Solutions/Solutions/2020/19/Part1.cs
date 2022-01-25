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

                continue;
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
}

public class Rule
{
    public int Id { get; }

    public char Character { get; set;  }

    public List<Rule> SubRules { get; }

    public List<Rule> LeftRules { get; }

    public List<Rule> RightRules { get; }

    public Rule(int id)
    {
        Id = id;

        SubRules = new List<Rule>();

        LeftRules = new List<Rule>();

        RightRules = new List<Rule>();
    }
}