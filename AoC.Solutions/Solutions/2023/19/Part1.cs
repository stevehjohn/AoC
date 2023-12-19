using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._19;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly Dictionary<string, List<Rule>> _workflows = new();
    
    public override string GetAnswer()
    {
        ParseInput();
        
        return "Unknown";
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
    }
}