using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._19;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput(false);

        var acceptWorkflows = Workflows.Where(w => w.Value.Any(r => r.Destination == "A"));

        long sum = 0;
        
        foreach (var rule in acceptWorkflows)
        {
            var result = CheckRule(rule.Key, rule.Value, 'x');

            var accepted = result;
            
            result = CheckRule(rule.Key, rule.Value, 'm');

            accepted = accepted == 0 ? result : accepted * (result == 0 ? 1 : result);
            
            result = CheckRule(rule.Key, rule.Value, 'a');

            accepted = accepted == 0 ? result : accepted * (result == 0 ? 1 : result);
            
            result = CheckRule(rule.Key, rule.Value, 's');

            accepted = accepted == 0 ? result : accepted * (result == 0 ? 1 : result);
            
            sum += accepted;
        }
        
        return sum.ToString();
    }

    private int CheckRule(string name, List<Rule> rules, char property)
    {
        rules.Reverse();

        var range = new Range(0, 4000);

        while (name != "in")
        {
            foreach (var rule in rules)
            {
                if (rule.Condition == '\0' || rule.Condition != property)
                {
                    continue;
                }

                if (range.Start < rule.Value && range.End > rule.Value)
                {
                    if (rule.Condition == '>')
                    {
                        range.Start = rule.Value;
                    }
                    else
                    {
                        range.End = rule.Value;
                    }
                }
            }
            
            rules = Workflows.Where(.Where(w => w.Value.Any(r => r.Destination == name));
        }

        return range.End - range.Start + 1;
    }
}