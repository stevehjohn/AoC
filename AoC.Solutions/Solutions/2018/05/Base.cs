using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2018._05;

public abstract class Base : Solution
{
    public override string Description => "Polymer reactions";

    protected static int ReactPolymer(string polymerString)
    {
        var polymer = new LinkedList<char>(polymerString.ToCharArray());

        var node = polymer.First;

        while (node is { Next: not null })
        {
            if (char.ToLower(node.Value) == char.ToLower(node.Next.Value) && node.Value != node.Next.Value)
            {
                var previous = node.Previous;

                polymer.Remove(node.Next);

                polymer.Remove(node);

                node = previous ?? polymer.First;
            }
            else
            {
                node = node.Next;
            }
        }

        return polymer.Count;
    }
}