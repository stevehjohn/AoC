using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._20;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var particles = ParseInput();

        var lastCount = particles.Count;

        while (true)
        {
            var positions = new Dictionary<(long X, long Y, long Z), int>();

            foreach (var particle in particles)
            {
                particle.Move();

                var position = particle.Position;

                var key = ((long) position.X, (long) position.Y, (long) position.Z);

                if (positions.TryGetValue(key, out var count))
                {
                    positions[key] = count + 1;
                }
                else
                {
                    positions.Add(key, 1);
                }
            }

            particles.RemoveAll(p =>
            {
                var position = p.Position;

                return positions[(position.X, position.Y, position.Z)] > 1;
            });

            if (particles.Count < Input.Length && particles.Count == lastCount)
            {
                break;
            }

            lastCount = particles.Count;
        }

        return particles.Count.ToString();
    }

    private List<Particle> ParseInput()
    {
        var particles = new List<Particle>(Input.Length);

        for (var i = 0; i < Input.Length; i++)
        {
            particles.Add(Particle.Parse(i, Input[i]));
        }

        return particles;
    }
}