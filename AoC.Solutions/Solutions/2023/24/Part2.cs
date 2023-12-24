using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._24;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();
        
        for (var left = 0; left < Hail.Count - 1; left++)
        {
            for (var right = left + 1; right < Hail.Count; right++)
            {
                if (CheckIfParallel(Hail[left], Hail[right]))
                {
                    Console.WriteLine("Parallel");
                }
            }
        }

        return "Unknown";
    }

    private static bool CheckIfParallel((DoublePoint Position, DoublePoint Velocity) left, (DoublePoint Position, DoublePoint Velocity) right)
    {
        var a1 = left.Velocity.Y / left.Velocity.X;
        var b1 = left.Position.Y - a1 * left.Position.X;
        var a2 = right.Velocity.Y / right.Velocity.X;
        var b2 = right.Position.Y - a2 * right.Position.X;

        if (EqualsWithinTolerance(a1, a2))
        {
            return ! EqualsWithinTolerance(b1, b2);
        }

        return false;
    }
}