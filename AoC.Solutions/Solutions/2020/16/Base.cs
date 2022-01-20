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

    protected int ValidateOtherTickets()
    {
        var invalid = 0;

        var toRemove = new List<List<int>>();

        foreach (var ticket in OtherTickets)
        {
            var ticketValid = true;

            foreach (var field in ticket)
            {
                var fieldValid = false;

                foreach (var rule in Rules)
                {
                    if (field >= rule.Rule1.Minimum && field <= rule.Rule1.Maximum || field >= rule.Rule2.Minimum && field <= rule.Rule2.Maximum)
                    {
                        fieldValid = true;

                        break;
                    }
                }

                if (! fieldValid)
                {
                    invalid += field;

                    ticketValid = false;
                }
            }

            if (! ticketValid)
            {
                toRemove.Add(ticket);
            }
        }

        toRemove.ForEach(t => OtherTickets.Remove(t));

        return invalid;
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