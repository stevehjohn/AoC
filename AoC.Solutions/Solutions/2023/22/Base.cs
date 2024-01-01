using AoC.Solutions.Common;
using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._22;

public abstract class Base : Solution
{
    public override string Description => "Sand slabs";

    protected const int MaxHeight = 300;
    
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
        if (Visualiser != null)
        {
            Visualiser.PuzzleStateChanged(new PuzzleState(Map, MaxHeight) { Settling = settling, DestroyBrickId = brickId });
        }
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
                
                found.ExceptWith(supported);

                for (var x = 0; x < 10; x++)
                {
                    for (var y = 0; y < 10; y++)
                    {
                        var brick = map[z, x, y];

                        if (found.Contains(brick))
                        {
                            map[z - 1, x, y] = brick;

                            map[z, x, y] = -1;

                            if (! move)
                            {
                                return 1;
                            }

                            if (z - 1 > HighestZ)
                            {
                                HighestZ = z - 1;
                            }

                            droppedIds.Add(brick);

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
                next = new Point(
                    next.X.Converge(end.X),
                    next.Y.Converge(end.Y),
                    next.Z.Converge(end.Z));

                Map[next.Z, next.X, next.Y] = Count;
            }
        }
    }
}