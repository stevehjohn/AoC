using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._19;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly Dictionary<string, List<Rule>> _workflows = new();

    private readonly List<Part> _parts = new();
    
    public override string GetAnswer()
    {
        ParseInput();

        var sum = 0;
        
        foreach (var part in _parts)
        {
            sum += ProcessPart(part);
        }
        
        return sum.ToString();
    }

    private int ProcessPart(Part part)
    {
        var name = "in";

        while (name is not ("A" or "R"))
        {
            var workflow = _workflows[name];

            foreach (var rule in workflow)
            {
                if (rule.Condition == '\0')
                {
                    name = rule.Destination;
                    
                    break;
                }

                var property = rule.Property switch
                {
                    'x' => part.X,
                    'm' => part.M,
                    'a' => part.A,
                    _ => part.S
                };

                bool pass;
                
                if (rule.Condition == '<')
                {
                    pass = property < rule.Value;
                }
                else
                {
                    pass = property > rule.Value;
                }

                if (pass)
                {
                    name = rule.Destination;
                }
            }
        }

        return name == "A" ? part.X + part.M + part.A + part.S : 0;
    }

    private void ParseInput()
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
            
            _workflows.Add(workflow, rulesList);

            l++;
        }

        l++;

        while (l < Input.Length - 1)
        {
            var parts = Input[l][1..^1].Split(',');

            var part = new Part
            {
                X = int.Parse(parts[0][2..]),
                M = int.Parse(parts[1][2..]),
                A = int.Parse(parts[2][2..]),
                S = int.Parse(parts[3][2..])
            };
            
            _parts.Add(part);
            
            l++;
        }
    }
}