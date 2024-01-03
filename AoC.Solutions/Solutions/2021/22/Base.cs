using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2021._22;

public abstract class Base : Solution
{
    public override string Description => "Reactor reboot";

    private readonly List<(Cuboid Cuboid, bool State)> _cuboids = new();

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

    protected void ProcessInput(int linesToProcess)
    {
        _cuboids.Add((_instructions.First().Cuboid, true));

        var instructions = _instructions.Skip(1).Take(linesToProcess - 1);

        foreach (var instruction in instructions)
        {
            ProcessInstruction(instruction);
        }
    }

    private void ProcessInstruction(Instruction instruction)
    {
        var toAdd = new List<(Cuboid Cuboid, bool Sign)>();

        foreach (var cuboid in _cuboids)
        {
            var intersection = cuboid.Cuboid.Intersects(instruction.Cuboid);

            if (intersection == null)
            {
                continue;
            }

            toAdd.Add((intersection, ! cuboid.State));
        }

        _cuboids.AddRange(toAdd);

        if (instruction.State)
        {
            _cuboids.Add((instruction.Cuboid, true));
        }
    }

    protected long GetVolume()
    {
        var volume = 0L;

        foreach (var item in _cuboids)
        {
            volume += (item.State ? 1 : -1) * item.Cuboid.Volume;
        }

        return volume;
    }
}