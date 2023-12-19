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
            var accepted = 0L;
            
            CheckRule(rule.Key, rule.Value, 'x');

            sum += accepted;
        }
        
        return sum.ToString();
    }

    private long CheckRule(string name, List<Rule> rules, char property)
    {
        return 0;
    }
}