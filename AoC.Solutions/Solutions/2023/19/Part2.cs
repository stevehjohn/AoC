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
            var result = CheckRule(rule.Key, rule.Value, 'x', new Range(1, 4000));

            var accepted = result;
            
            // result = CheckRule(rule.Key, rule.Value, 'm', new Range(1, 4000));
            //
            // accepted = accepted == 0 ? result : accepted * (result == 0 ? 1 : result);
            //
            // result = CheckRule(rule.Key, rule.Value, 'a', new Range(1, 4000));
            //
            // accepted = accepted == 0 ? result : accepted * (result == 0 ? 1 : result);
            //
            // result = CheckRule(rule.Key, rule.Value, 's', new Range(1, 4000));
            //
            // accepted = accepted == 0 ? result : accepted * (result == 0 ? 1 : result);
            
            sum += accepted;
        }
        
        return sum.ToString();
    }

    private int CheckRule(string name, List<Rule> rules, char property, Range range)
    {
        rules.Reverse();

        Console.WriteLine($"{name} ({property}): {range}");

        var found = false;
        
        foreach (var rule in rules)
        {
            if (rule.Destination != "A" && ! found)
            {
                continue;
            }

            found = true;

            if (rule.Condition == '\0')
            {
                if (rule.Destination == "A")
                {
                    continue;
                }

                if (rule.Destination == "R")
                {
                    break;
                }
            }

            if (rule.Property == property)
            {
                if (range.Start < rule.Value && range.End > rule.Value)
                {
                    if (rule.Condition == '>')
                    {
                        range.Start = rule.Value + 1;
                    }
                    else
                    {
                        range.End = rule.Value - 1;
                    }
                }
            }
        }

        foreach (var workflow in Workflows.Where(w => w.Value.Any(r => r.Destination == name)))
        {
            CheckRule(workflow.Key, workflow.Value, property, range);
        }

        return range.End - range.Start + 1;
    }
}