using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._16;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        ValidateOtherTickets();

        var fieldNames = CalculateFields();

        var result = 1L;

        for (var i = 0; i < YourTicket.Count; i++)
        {
            if (fieldNames[i].StartsWith("departure"))
            {
                result *= YourTicket[i];
            }
        }

        return result.ToString();
    }

    private List<string> CalculateFields()
    {
        var rules = new List<Rule>[YourTicket.Count];

        for (var i = 0; i < YourTicket.Count; i++)
        {
            rules[i] = [..Rules];
        }

        while (rules.Any(r => r.Count > 1))
        {
            foreach (var ticket in OtherTickets)
            {
                foreach (var rule in Rules)
                {
                    for (var i = 0; i < ticket.Count; i++)
                    {
                        var field = ticket[i];

                        if (! (field >= rule.Rule1.Minimum && field <= rule.Rule1.Maximum) && ! (field >= rule.Rule2.Minimum && field <= rule.Rule2.Maximum))
                        {
                            rules[i].Remove(rule);
                        }
                    }
                }
            }

            foreach (var rule in rules)
            {
                if (rule.Count == 1)
                {
                    foreach (var removeFrom in rules)
                    {
                        if (removeFrom == rule)
                        {
                            continue;
                        }

                        removeFrom.Remove(rule[0]);
                    }
                }
            }
        }

        return rules.Select(r => r[0].Name).ToList();
    }
}