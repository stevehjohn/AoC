using AoC.Solutions.Common;
using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._22;

public abstract class Base : Solution
{
    public override string Description => "Sand slabs";

    private readonly int[,,] _map = new int[300, 10, 10];

    protected readonly HashSet<(int Id, int SupportedById)> Supported = new();

    protected int Count;
    
    protected void BuildStructure()
    {
        for (var z = 2; z < 300; z++)
        {
            for (var x = 0; x < 10; x++)
            {
                for (var y = 0; y < 10; y++)
                {
                    var brick = _map[z, x, y];

                    if (brick == 0)
                    {
                        continue;
                    }

                    var below = _map[z - 1, x, y];
                    
                    if (below != 0 && below != brick)
                    {
                        Supported.Add((brick, below));
                    }
                }
            }
        }
    }

    protected void Dump()
    {
        for (var z = 150; z > 0; z--)
        {
            for (int y = 0; y < 10; y++)
            {
                var found = 0;
                
                for (int x = 0; x < 10; x++)
                {
                    if (_map[z, x, y] != 0)
                    {
                        found = _map[z, x, y];
                        
                        break;
                    }
                }
    
                if (found > 0)
                {
                    Console.Write((char) (255 + found));
                }
                else
                {
                    Console.Write(' ');
                }
            }
                
            Console.WriteLine();
        }
    }

    protected void SettleBricks()
    {
        var found = new HashSet<int>();

        var supported = new HashSet<int>();

        var dropped = true;

        while (dropped)
        {
            dropped = false;

            for (var z = 2; z < 300; z++)
            {
                for (var x = 0; x < 10; x++)
                {
                    for (var y = 0; y < 10; y++)
                    {
                        var brick = _map[z, x, y];

                        if (brick == 0)
                        {
                            continue;
                        }

                        found.Add(brick);

                        if (_map[z - 1, x, y] != 0)
                        {
                            supported.Add(brick);
                        }
                    }
                }

                for (var x = 0; x < 10; x++)
                {
                    for (var y = 0; y < 10; y++)
                    {
                        var brick = _map[z, x, y];

                        if (found.Contains(brick) && ! supported.Contains(brick))
                        {
                            _map[z - 1, x, y] = brick;

                            _map[z, x, y] = 0;

                            dropped = true;
                        }
                    }
                }

                found.Clear();

                supported.Clear();
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

            _map[start.Z, start.X, start.Y] = Count;

            while (! start.Equals(end))
            {
                start = new Point(
                    start.X.Converge(end.X),
                    start.Y.Converge(end.Y),
                    start.Z.Converge(end.Z));

                _map[start.Z, start.X, start.Y] = Count;
            }
        }
    }
}