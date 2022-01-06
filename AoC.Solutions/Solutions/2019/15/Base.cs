#define DUMP
using AoC.Solutions.Infrastructure;
using AoC.Solutions.Solutions._2019.Computer;

namespace AoC.Solutions.Solutions._2019._15;

public abstract class Base : Solution
{
    public override string Description => "Oxygen repair droid";

    private readonly Dictionary<int, Dictionary<int, CellType>> _map = new();

    protected int OxygenX = int.MinValue;

    protected int OxygenY = int.MinValue;

    public bool[,] GetMap()
    {
#if DEBUG && DUMP
        Console.Clear();

        Console.CursorVisible = false;
#endif

        var cpu = new Cpu();

        cpu.Initialise();

        cpu.LoadProgram(Input);

        var x = 0;

        var y = 0;

        SetCellType(x, y, (CellType) 1);

        while (true)
        {
            var move = GetBestMove(x, y);

            cpu.UserInput.Enqueue(move.Direction);

            cpu.Run();

            var response = cpu.UserOutput.Dequeue();

            if (response == 0)
            {
                SetCellType(move.NewX, move.NewY, CellType.Wall);

                continue;
            }

            x = move.NewX;

            y = move.NewY;

            SetCellType(x, y, move.CellType + 1);

            if (response == 2)
            {
                OxygenX = x;

                OxygenY = y;

                if (! Explored())
                {
                    continue;
                }

                break;
            }

            if (OxygenX > int.MinValue)
            {
                if (Math.Abs(x - OxygenX) == 1 && y == OxygenY || Math.Abs(y - OxygenY) == 1 && x == OxygenX)
                {
                    if (Explored())
                    {
                        break;
                    }
                }
            }
        }

#if DEBUG && DUMP
        Dump();
#endif
    }

    private bool Explored()
    {
        var xMin = _map.Min(m => m.Key);

        var xMax = _map.Max(m => m.Key);

        var yMin = _map.SelectMany(m => m.Value.Keys).Min();

        var yMax = _map.SelectMany(m => m.Value.Keys).Max();

        for (var x = xMin + 1; x < xMax; x++)
        {
            for (var y = yMin + 1; y < yMax; y++)
            {
                if (GetCellType(x, y) != CellType.Unknown)
                {
                    continue;
                }

                if (GetCellType(x - 1, y) != CellType.Wall || GetCellType(x, y - 1) != CellType.Wall || GetCellType(x + 1, y) != CellType.Wall || GetCellType(x, y + 1) != CellType.Wall)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private (int NewX, int NewY, CellType CellType, int Direction) GetBestMove(int x, int y)
    {
        var neighbors = new List<(int X, int Y, CellType CellType, int Direction)>
                        {
                            (x - 1, y, GetCellType(x - 1, y), 3),
                            (x, y - 1, GetCellType(x, y - 1), 1),
                            (x + 1, y, GetCellType(x + 1, y), 4),
                            (x, y + 1, GetCellType(x, y + 1), 2)
                        };

        var best = neighbors.OrderBy(n => (int) n.CellType).First();

        if (neighbors.Count(n => n.CellType == CellType.Wall) == 3)
        {
            SetCellType(x, y, CellType.DeadEnd);
        }

        return best;
    }

    private CellType GetCellType(int x, int y)
    {
        if (! _map.ContainsKey(x))
        {
            return CellType.Unknown;
        }

        if (! _map[x].ContainsKey(y))
        {
            return CellType.Unknown;
        }

        return _map[x][y];
    }

    private void SetCellType(int x, int y, CellType cellType)
    {
        if (! _map.ContainsKey(x))
        {
            _map.Add(x, new Dictionary<int, CellType>());
        }

        if (! _map[x].ContainsKey(y))
        {
            _map[x].Add(y, cellType);

            return;
        }

        _map[x][y] = cellType;
    }

#if DEBUG && DUMP
    private void Dump(int pX = int.MinValue, int pY = int.MinValue)
    {
        Console.CursorTop = 1;

        var xMin = _map.Min(m => m.Key);

        var xMax = _map.Max(m => m.Key);

        var yMin = _map.SelectMany(m => m.Value.Keys).Min();

        var yMax = _map.SelectMany(m => m.Value.Keys).Max();

        for (var y = yMin; y <= yMax; y++)
        {
            Console.Write(' ');

            for (var x = xMin; x <= xMax; x++)
            {
                if (x == 0 && y == 0)
                {
                    Console.Write('S');

                    continue;
                }

                if (x == pX && y == pY)
                {
                    Console.Write('⧱');

                    continue;
                }

                if (x == OxygenX && y == OxygenY)
                {
                    Console.Write('O');

                    continue;
                }

                var type = GetCellType(x, y);

                var c = (int) type switch
                {
                    (int) CellType.Wall => '█',
                    (int) CellType.Unknown => '?',
                    _ => ' ' //(char) ('0' + type)
                };

                Console.Write(c);
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }
#endif
}