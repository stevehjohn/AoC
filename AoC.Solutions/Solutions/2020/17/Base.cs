using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2020._17;

public abstract class Base : Solution
{
    public override string Description => "Conway cubes";

    protected readonly HashSet<Point4D> ActiveCubes = [];

    protected void ParseInput()
    {
        ActiveCubes.Clear();

        for (var y = 0; y < Input.Length; y++)
        {
            for (var x = 0; x < Input[y].Length; x++)
            {
                if (Input[y][x] == '#')
                {
                    ActiveCubes.Add(new Point4D(x, y));
                }
            }
        }
    }

    protected void RunCycle(bool exploreW = false)
    {
        var counts = new Dictionary<Point4D, int>();

        foreach (var cube in ActiveCubes)
        {
            for (var dw = exploreW ? -1 : 0; dw <= (exploreW ? 1 : 0); dw++)
            {
                for (var dz = -1; dz <= 1; dz++)
                {
                    for (var dy = -1; dy <= 1; dy++)
                    {
                        for (var dx = -1; dx <= 1; dx++)
                        {
                            if (dx == 0 && dy == 0 && dz == 0 && dw == 0)
                            {
                                continue;
                            }

                            var neighbor = new Point4D(cube.X + dx, cube.Y + dy, cube.Z + dz, cube.W + dw);

                            counts.TryGetValue(neighbor, out var count);

                            counts[neighbor] = count + 1;
                        }
                    }
                }
            }
        }

        var next = new HashSet<Point4D>();

        foreach (var (position, neighbors) in counts)
        {
            if (neighbors == 3 || (neighbors == 2 && ActiveCubes.Contains(position)))
            {
                next.Add(position);
            }
        }

        ActiveCubes.Clear();
        
        ActiveCubes.UnionWith(next);
    }
}
