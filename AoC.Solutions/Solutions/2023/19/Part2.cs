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

    private long CheckRule(string name, List<Rule> rules, char property)
    {
        rules.Reverse();

        var ranges = new List<Range> { new(0, 4000) };

        foreach (var rule in rules)
        {
            if (rule.Condition == '\0' || rule.Condition != property)
            {
                continue;
            }
            
            var newRanges = new List<Range>();
            
            foreach (var range in ranges)
            {
                
            }

            ranges = newRanges;
        }

        return ranges.Sum(r => r.End - r.Start + 1);
    }
}