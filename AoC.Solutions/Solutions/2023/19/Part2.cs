using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._19;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput(false);

        var result = CheckRule("in", new Dictionary<char, (int Start, int End)>
        {
            { 'x', (1, 4000) },
            { 'm', (1, 4000) },
            { 'a', (1, 4000) },
            { 's', (1, 4000) }
        });
        
        return result.ToString();
    }

    private long CheckRule(string name, Dictionary<char, (int Start, int End)> ranges)
    {
        switch (name)
        {
            case "R":
                return 0;
            case "A":
                return ranges.Values.Aggregate<(int Start, int End), long>(1, (total, range) => total * (range.End - range.Start + 1));
        }

        var workflow = Workflows[name];

        var result = 0L;
        
        foreach (var rule in workflow)
        {
            if (rule.Condition == '\0')
            {
                result += CheckRule(rule.Destination, ranges);
                
                continue;
            }

            var range = ranges[rule.Property];
            
            switch (rule.Condition)
            {
                case '<':
                    if (range.End < rule.Value)
                    {
                        result += CheckRule(rule.Destination, ranges);

                        return result;
                    }

                    if (range.Start < rule.Value)
                    {
                        var newRanges = new Dictionary<char, (int Start, int End)>(ranges)
                        {
                            [rule.Property] = (range.Start, rule.Value - 1)
                        };

                        result += CheckRule(rule.Destination, newRanges);

                        ranges[rule.Property] = (rule.Value, range.End);
                    }

                    break;
                
                case '>':
                    if (range.Start > rule.Value)
                    {
                        result += CheckRule(rule.Destination, ranges);

                        return result;
                    }

                    if (range.End > rule.Value)
                    {
                        var newRanges = new Dictionary<char, (int Start, int End)>(ranges)
                        {
                            [rule.Property] = (rule.Value + 1, range.End)
                        };

                        result += CheckRule(rule.Destination, newRanges);

                        ranges[rule.Property] = (range.Start, rule.Value);
                    }

                    break;
            }
        }

        return result;
    }
}