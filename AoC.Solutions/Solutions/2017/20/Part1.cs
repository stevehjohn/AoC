using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._20;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var particles = ParseInput();

        var closest = particles.OrderBy(p => Math.Abs(p.Acceleration.X) + Math.Abs(p.Acceleration.Y) + Math.Abs(p.Acceleration.Z))
                               .ThenBy(p => Math.Abs(p.Velocity.X) + Math.Abs(p.Velocity.Y) + Math.Abs(p.Velocity.Z))
                               .First()
                               .Id;

        return closest.ToString();
    }

    private List<Particle> ParseInput()
    {
        var particles = new List<Particle>();

        var i = 0;

        foreach (var line in Input)
        {
            particles.Add(Particle.Parse(i, line));

            i++;
        }

        return particles;
    }
}