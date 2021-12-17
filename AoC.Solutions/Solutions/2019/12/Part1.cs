using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._12;

[UsedImplicitly]
public class Part1 : Base
{
    public override string Description => "Orbital simulation";

    public override string GetAnswer()
    {
        ParseInput();

        for (var step = 0; step < 1000; step++)
        {
            for (var comparer = 0; comparer < Moons.Count; comparer++)
            {
                for (var comparee = comparer + 1; comparee < Moons.Count; comparee++)
                {
                    ApplyGravity(Moons[comparer], Moons[comparee]);
                }
            }

            for (var i = 0; i < Moons.Count; i++)
            {
                Moons[i].Position.X += Moons[i].Velocity.X;

                Moons[i].Position.Y += Moons[i].Velocity.Y;
                
                Moons[i].Position.Z += Moons[i].Velocity.Z;
            }
        }

        var energy = Moons.Sum(m => m.Energy);

        return energy.ToString();
    }

    private static void ApplyGravity(Moon comparer, Moon comparee)
    {
        comparer.Velocity.X += GetGravity(comparer.Position.X, comparee.Position.X);

        comparer.Velocity.Y += GetGravity(comparer.Position.Y, comparee.Position.Y);

        comparer.Velocity.Z += GetGravity(comparer.Position.Z, comparee.Position.Z);

        comparee.Velocity.X += GetGravity(comparee.Position.X, comparer.Position.X);

        comparee.Velocity.Y += GetGravity(comparee.Position.Y, comparer.Position.Y);

        comparee.Velocity.Z += GetGravity(comparee.Position.Z, comparer.Position.Z);
    }

    private static int GetGravity(int left, int right)
    {
        if (left < right)
        {
            return 1;
        }

        if (left > right)
        {
            return -1;
        }

        return 0;
    }
}