using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._24;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var wrong = Gates.Where(g => g.Key[0] == 'z' && g.Key != "z45" && g.Value.Type != Type.XOR).Select(g => g.Key).ToHashSet();

        wrong = wrong.Union(Gates.Where(g => g.Key[0] != 'z'
                                             && g.Value.Left[0] is not ('x' or 'y')
                                             && g.Value.Right[0] is not ('x' or 'y')
                                             && g.Value.Type == Type.XOR).Select(g => g.Key)).ToHashSet();

        foreach (var left in Gates.Where(g => g.Value.Type == Type.AND && g.Value.Left != "x00" && g.Value.Right != "x00"))
        {
            foreach (var right in Gates)
            {
                if ((left.Key == right.Value.Left || left.Key == right.Value.Right) && right.Value.Type != Type.OR)
                {
                    wrong.Add(left.Key);
                }
            }
        }

        return string.Join(',', wrong.Order());
    }
}