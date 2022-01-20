using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2020._16;

public abstract class Base : Solution
{
    public override string Description => "Ticket deciphering";

    protected List<Rule> Rules = new();

    protected List<int> YourTicket;

    protected List<List<int>> OtherTickets = new();

    protected void ParseInput()
    {
        var phase = 0;

        for (var i = 0; i < Input.Length; i++)
        {
            var line = Input[i];

            if (string.IsNullOrWhiteSpace(line))
            {
                phase++;

                i++;

                continue;
            }

            if (phase == 0)
            {
                Rules.Add(ParseRule(line));

                continue;
            }

            if (phase == 1)
            {
                YourTicket = ParseTicket(line);

                continue;
            }

            OtherTickets.Add(ParseTicket(line));
        }
    }

    private static List<int> ParseTicket(string input)
    {
        return input.Split(',', StringSplitOptions.TrimEntries).Select(int.Parse).ToList();
    }

    private static Rule ParseRule(string input)
    {
        var split = input.Split(':', StringSplitOptions.TrimEntries);

        var rules = split[1].Split(" or ", StringSplitOptions.TrimEntries);

        var rule = rules[0].Split('-', StringSplitOptions.TrimEntries);

        var rule1 = new Range(int.Parse(rule[0]), int.Parse(rule[1]));

        rule = rules[1].Split('-', StringSplitOptions.TrimEntries);

        var rule2 = new Range(int.Parse(rule[0]), int.Parse(rule[1]));

        return new Rule(split[0], rule1, rule2);
    }
}

public class Rule
{
    public string Name { get; }

    public Range Rule1 { get; }

    public Range Rule2 { get; }

    public Rule(string name, Range rule1, Range rule2)
    {
        Name = name;

        Rule1 = rule1;

        Rule2 = rule2;
    }
}

public class Range
{
    public int Minimum { get; }

    public int Maximum { get; }

    public Range(int minimum, int maximum)
    {
        Minimum = minimum;

        Maximum = maximum;
    }
}