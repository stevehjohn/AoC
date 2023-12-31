using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._19;

public abstract class Base : Solution
{
    public override string Description => "Aplenty";
    
    protected readonly Dictionary<string, List<Rule>> Workflows = new();

    protected readonly List<Part> Parts = new();

    protected void ParseInput(bool getParts = true)
    {
        var l = 0;

        while (! string.IsNullOrWhiteSpace(Input[l]))
        {
            var parts = Input[l][..^1].Split('{');

            var workflow = parts[0];
            
            var rulesString = parts[1].Split(',');

            var rulesList = new List<Rule>();

            foreach (var item in rulesString)
            {
                if (! item.Contains(':'))
                {
                    rulesList.Add(new Rule { Destination = item});
                    
                    continue;
                }

                var rule = new Rule
                {
                    Property = item[0],
                    Condition = item[1]
                };
                
                var ruleParts = item.Split(':');

                rule.Value = int.Parse(ruleParts[0][2..]);

                rule.Destination = ruleParts[1];
                
                rulesList.Add(rule);
            }
            
            Workflows.Add(workflow, rulesList);

            l++;
        }

        if (! getParts)
        {
            return;
        }

        l++;

        while (l < Input.Length)
        {
            var parts = Input[l][1..^1].Split(',');

            var part = new Part
            {
                X = int.Parse(parts[0][2..]),
                M = int.Parse(parts[1][2..]),
                A = int.Parse(parts[2][2..]),
                S = int.Parse(parts[3][2..])
            };
            
            Parts.Add(part);
            
            l++;
        }
    }
}