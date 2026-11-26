using System.Numerics;
using AoC.Solutions.Common;
using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._22;

public abstract class Base : Solution
{
    public override string Description => "Sand slabs";

    protected const int MaxHeight = 290;

    protected readonly int[,,] Map = new int[MaxHeight, 10, 10];

    protected int Count;

    protected int HighestZ;

    protected readonly IVisualiser<PuzzleState> Visualiser;

    protected Base()
    {
    }

    protected Base(IVisualiser<PuzzleState> visualiser)
    {
        Visualiser = visualiser;
    }

    protected void Visualise(bool settling, int brickId = 0)
    {
        Visualiser?.PuzzleStateChanged(new PuzzleState(Map, MaxHeight) { Settling = settling, DestroyBrickId = brickId });
    }

    protected int SettleBricks(int[,,] map, bool move = true, bool suppressVisualise = false)
    {
        var found = new HashSet<int>();

        var supported = new HashSet<int>();

        var dropped = true;

        var droppedIds = new HashSet<int>();

        while (dropped)
        {
            dropped = false;

            for (var z = 2; z < MaxHeight; z++)
            {
                for (var x = 0; x < 10; x++)
                {
                    for (var y = 0; y < 10; y++)
                    {
                        var brick = map[z, x, y];

                        if (brick < 1)
                        {
                            continue;
                        }

                        found.Add(brick);

                        if (map[z - 1, x, y] > 0)
                        {
                            supported.Add(brick);
                        }
                    }
                }

                if (found.Count == 0 || found.Count == supported.Count)
                {
                    continue;
                }

                if (! move)
                {
                    return 1;
                }

                found.ExceptWith(supported);

                droppedIds.UnionWith(found);

                for (var x = 0; x < 10; x++)
                {
                    for (var y = 0; y < 10; y++)
                    {
                        var brick = map[z, x, y];

                        if (found.Contains(brick))
                        {
                            map[z - 1, x, y] = brick;

                            map[z, x, y] = -1;

                            if (z - 1 > HighestZ)
                            {
                                HighestZ = z - 1;
                            }

                            dropped = true;
                        }
                    }
                }

                found.Clear();

                supported.Clear();
            }

            if (move && ! suppressVisualise)
            {
                Visualise(true);
            }
        }

        return droppedIds.Count;
    }

    protected void ParseInput()
    {
        var items = new List<(Point Start, Point End)>();

        foreach (var line in Input)
        {
            var parts = line.Split('~');

            var start = Point.Parse(parts[0]);

            var end = Point.Parse(parts[1]);

            if (end.Z < start.Z)
            {
                (start, end) = (end, start);
            }

            items.Add((start, end));
        }

        foreach (var (start, end) in items.OrderBy(i => i.Start.Z))
        {
            Count++;

            Map[start.Z, start.X, start.Y] = Count;

            var next = start;

            while (! next.Equals(end))
            {
                next = new Point(next.X.Converge(end.X), next.Y.Converge(end.Y), next.Z.Converge(end.Z));

                Map[next.Z, next.X, next.Y] = Count;
            }
        }
    }

    protected long SolveFast(bool part2)
    {
        var bricks = Input.Select(line =>
        {
            var parts = line.Split('~');

            var a = Point.Parse(parts[0]);
            
            var b = Point.Parse(parts[1]);

            return (MinX: Math.Min(a.X, b.X), MaxX: Math.Max(a.X, b.X), MinY: Math.Min(a.Y, b.Y), MaxY: Math.Max(a.Y, b.Y), MinZ: Math.Min(a.Z, b.Z), MaxZ: Math.Max(a.Z, b.Z));
        }).OrderBy(b => b.MinZ).ToArray();

        var count = bricks.Length;

        var height = new int[10, 10];

        var owner = new int[10, 10];

        var critical = new bool[count + 1];

        var supports = new HashSet<int>();

        var words = (count + 63) / 64;

        var causes = new ulong[count + 1][];

        var total = 0L;

        for (var id = 1; id <= count; id++)
        {
            var brick = bricks[id - 1];

            var bottom = 0;

            supports.Clear();

            for (var x = brick.MinX; x <= brick.MaxX; x++)
            {
                for (var y = brick.MinY; y <= brick.MaxY; y++)
                {
                    var h = height[x, y];

                    if (h > bottom)
                    {
                        bottom = h;

                        supports.Clear();
                    }

                    if (h == bottom && owner[x, y] != 0)
                    {
                        supports.Add(owner[x, y]);
                    }
                }
            }

            if (supports.Count == 1)
            {
                critical[supports.First()] = true;
            }

            if (part2)
            {
                var bits = new ulong[words];

                var first = true;

                foreach (var support in supports)
                {
                    var source = causes[support];

                    if (first)
                    {
                        source.CopyTo(bits, 0);

                        first = false;
                    }
                    else
                    {
                        for (var w = 0; w < words; w++)
                        {
                            bits[w] &= source[w];
                        }
                    }
                }

                for (var w = 0; w < words; w++)
                {
                    total += BitOperations.PopCount(bits[w]);
                }

                bits[(id - 1) >> 6] |= 1UL << ((id - 1) & 63);

                causes[id] = bits;
            }

            var top = bottom + brick.MaxZ - brick.MinZ + 1;

            for (var x = brick.MinX; x <= brick.MaxX; x++)
            {
                for (var y = brick.MinY; y <= brick.MaxY; y++)
                {
                    height[x, y] = top;

                    owner[x, y] = id;
                }
            }
        }

        return part2 ? total : count - critical.Count(c => c);
    }
}