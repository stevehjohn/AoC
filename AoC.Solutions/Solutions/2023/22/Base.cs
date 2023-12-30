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

    private int _highestZ;

    private readonly IVisualiser<PuzzleState> _visualiser;

    protected Base()
    {
    }

    protected Base(IVisualiser<PuzzleState> visualiser)
    {
        _visualiser = visualiser;
    }

    protected void Visualise(bool settling, int brickId = 0)
    {
        if (_visualiser != null)
        {
            _visualiser.PuzzleStateChanged(new PuzzleState(Map, MaxHeight) { Settling = settling, DestroyBrickId = brickId });
        }
    }

    protected int SettleBricks(int[,,] map, bool move = true)
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

                        if (brick == 0)
                        {
                            continue;
                        }

                        found.Add(brick);

                        if (map[z - 1, x, y] != 0)
                        {
                            supported.Add(brick);
                        }
                    }
                }

                if (found.Count == 0)
                {
                    continue;
                }

                for (var x = 0; x < 10; x++)
                {
                    for (var y = 0; y < 10; y++)
                    {
                        var brick = map[z, x, y];

                        if (found.Contains(brick) && ! supported.Contains(brick))
                        {
                            map[z - 1, x, y] = brick;

                            map[z, x, y] = 0;

                            if (! move)
                            {
                                return 1;
                            }

                            if (z - 1 > _highestZ)
                            {
                                _highestZ = z - 1;
                            }

                            droppedIds.Add(brick);

                            dropped = true;
                        }
                    }
                }
                
                found.Clear();

                supported.Clear();
            }

            if (move)
            {
                Visualise(true);
            }
        }
        
        return droppedIds.Count;
    }

    protected void WalkUpMap(Action<int, int, int> action)
    {
        for (var z = 1; z < _highestZ; z++)
        {
            for (var x = 0; x < 10; x++)
            {
                for (var y = 0; y < 10; y++)
                {
                    action(x, y, z);
                }
            }
        }
    }

    protected void ParseInput()
    {
        foreach (var line in Input)
        {
            var parts = line.Split('~');

            var start = Point.Parse(parts[0]);

            var end = Point.Parse(parts[1]);

            if (end.Z < start.Z)
            {
                (start, end) = (end, start);
            }

            Count++;

            Map[start.Z, start.X, start.Y] = Count;

            while (! start.Equals(end))
            {
                start = new Point(
                    start.X.Converge(end.X),
                    start.Y.Converge(end.Y),
                    start.Z.Converge(end.Z));

                Map[start.Z, start.X, start.Y] = Count;
            }
        }
    }
}