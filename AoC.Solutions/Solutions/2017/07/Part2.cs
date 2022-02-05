using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._07;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var node = FindUnbalancedNode(RootNode);

        return node.Name;
    }

    private Node FindUnbalancedNode(Node node)
    {
        foreach (var child in node.Children)
        {
            var unbalanced = FindUnbalancedNode(child);

            if (unbalanced != null)
            {
                return unbalanced;
            }
        }

        node.Weight += node.Children.Sum(n => n.Weight);

        if (node.Children.Select(n => n.Weight).Distinct().Count() > 1)
        {
            return node;
        }

        return null;
    }
}