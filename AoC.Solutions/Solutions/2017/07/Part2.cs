using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._07;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var node = FindUnbalancedNode(RootNode);

        var max = node.Children.Max(c => c.TotalWeight);

        var min = node.Children.Min(c => c.TotalWeight);

        var delta = max - min;

        int correctWeight;

        if (node.Children.Count(c => c.TotalWeight == max) == 1)
        {
            correctWeight = node.Children.Single(c => c.TotalWeight == max).Weight - delta;
        }
        else
        {
            correctWeight = node.Children.Single(c => c.TotalWeight == min).Weight + delta;
        }

        return correctWeight.ToString();
    }

    private static Node FindUnbalancedNode(Node node)
    {
        foreach (var child in node.Children)
        {
            var unbalanced = FindUnbalancedNode(child);

            if (unbalanced != null)
            {
                return unbalanced;
            }
        }

        node.TotalWeight += node.Children.Sum(n => n.TotalWeight);

        if (node.Children.Select(n => n.TotalWeight).Distinct().Count() > 1)
        {
            return node;
        }

        return null;
    }
}