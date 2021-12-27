using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2021._22;

public abstract class Base : Solution
{
    public override string Description => "Reactor reboot";

    private readonly List<Cuboid> _cuboids = new();

    private readonly List<Instruction> _instructions = new ();

    public void ParseInput()
    {
        foreach (var line in Input)
        {
            var split = line.Split(' ', StringSplitOptions.TrimEntries);

            var state = split[0] == "on";

            var pairs = split[1].Split(',', StringSplitOptions.TrimEntries);

            var x = ProcessPair(pairs[0]);

            var y = ProcessPair(pairs[1]);

            var z = ProcessPair(pairs[2]);

            var cuboid = new Cuboid(new Point(x.Left, y.Left, z.Left), new Point(x.Right, y.Right, z.Right));

            _instructions.Add(new Instruction(state, cuboid));
        }
    }

    private static (int Left, int Right) ProcessPair(string pair)
    {
        var split = pair.Substring(2).Split("..", StringSplitOptions.TrimEntries);

        return (int.Parse(split[0]), int.Parse(split[1]));
    }

    protected void ProcessInput()
    {
        _cuboids.Add(_instructions.First().Cuboid);

        foreach (var instruction in _instructions.Skip(1))
        {
            var intersections = GetIntersections(instruction.Cuboid);

            if (intersections.Count == 0 && instruction.State)
            {
                _cuboids.Add(instruction.Cuboid);

                continue;
            }

            /*
             * Split the intersecting cuboids leaving non intersecting parts in the list.
             *   (remove both original cuboids, add their sub-cuboids but not the intersection.
             * If the instruction 'off', do not add the intersection to the list.
             */

            // Just for testing... need to split the cuboids.
            _cuboids.AddRange(intersections);
        }
    }

    private List<Cuboid> GetIntersections(Cuboid other)
    {
        var result = new List<Cuboid>();

        foreach (var cuboid in _cuboids)
        {
            var intersection = cuboid.Intersects(other);

            if (intersection != null)
            {
                result.Add(intersection);
            }
        }

        return result;
    }
}