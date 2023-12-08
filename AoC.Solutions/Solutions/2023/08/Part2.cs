using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._08;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var pathLengths = GetPathLengths();
        
        return LowestCommonMultiple(pathLengths).ToString();
    }

    private List<long> GetPathLengths()
    {
        var nodes = Map.Where(n => n.Key.EndsWith('A')).Select(n => n.Key).ToArray();

        var pathLengths = new List<long>();
        
        foreach (var node in nodes)
        {
            pathLengths.Add(WalkMap(node));   
        }

        return pathLengths;
    }

    private long WalkMap(string node)
    {
        var steps = 0L;

        var step = 0;

        while (true)
        {
            steps++;
            
            node = Steps[step] == 'L' ? Map[node].Left : Map[node].Right;

            if (node.EndsWith('Z'))
            {
                break;
            }

            step++;

            if (step == Steps.Length)
            {
                step = 0;
            }
        }
        
        return steps;
    }

    private static long LowestCommonMultiple(List<long> input)
    {
        if (input.Count == 2)
        {
            var left = input[0];

            var right = input[1];

            return left * right / GreatestCommonFactor(left, right);
        }

        var lowestCommonMultiple = LowestCommonMultiple(input.Take(2).ToList());

        var remaining = input.Skip(2).ToList();

        remaining.Add(lowestCommonMultiple);

        return LowestCommonMultiple(remaining);
    }

    private static long GreatestCommonFactor(long left, long right)
    {
        while (left != right)
        {
            if (left > right)
            {
                left -= right;
            }
            else
            {
                right -= left;
            }
        }

        return left;
    }
}