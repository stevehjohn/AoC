using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._12;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var initialStates = Moons.Select(m => new Moon(m)).ToList();

        var cycleCounts = new List<int>[Moons.Count];

        for (var i = 0; i < Moons.Count; i++)
        {
            cycleCounts[i] = new List<int>();
        }

        var cycle = 0;

        while (true)
        {
            RunCycle();

            cycle++;

            for (var i = 0; i < Moons.Count; i++)
            {
                if (Moons[i].Position.X == initialStates[i].Position.X && Moons[i].Velocity.X == initialStates[i].Velocity.X)
                {
                    cycleCounts[i].Add(cycle);
                }

                if (Moons[i].Position.Y == initialStates[i].Position.Y && Moons[i].Velocity.Y == initialStates[i].Velocity.Y)
                {
                    cycleCounts[i].Add(cycle);
                }

                if (Moons[i].Position.Z == initialStates[i].Position.Z && Moons[i].Velocity.Z == initialStates[i].Velocity.Z)
                {
                    cycleCounts[i].Add(cycle);
                }
            }

            if (cycleCounts.All(c => c.Count > 0))
            {
                break;
            }
        }

        return "TESTING";
    }
}