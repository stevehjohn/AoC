using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._20;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var particles = ParseInput();

        var lastCount = 1000;

        while (true)
        {
            foreach (var particle in particles)
            {
                particle.Move();
            }

            var toRemove = new List<Particle>();

            foreach (var particle in particles)
            {
                var others = particles.Where(p => p.Position.Equals(particle.Position) && p != particle).ToList();

                if (others.Count > 0)
                {
                    toRemove.Add(particle);

                    toRemove.AddRange(others);
                }
            }

            toRemove.ForEach(p => particles.Remove(p));

            if (particles.Count < 1_000 && particles.Count == lastCount)
            {
                break;
            }

            lastCount = particles.Count;
        }

        return lastCount.ToString();
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