using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;
using AoC.Solutions.Solutions._2019.Computer;

namespace AoC.Solutions.Solutions._2019._15;

public abstract class Base : Solution
{
    protected const string Part1ResultFile = "2019.15.1.result";

    public override string Description => "Oxygen repair droid (CPU used unmodified)";

    protected bool[,] Map { get; set; }

    protected Point Origin { get; set; }

    protected Point Destination { get; set; }

    protected int Width { get; set; }

    protected int Height { get; set; }

    private readonly Dictionary<int, Dictionary<int, CellType>> _map = new();

    private int _oxygenX = int.MinValue;

    private int _oxygenY = int.MinValue;

    protected void GetMap()
    {
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
                _oxygenX = x;

                _oxygenY = y;

                if (! Explored())
                {
                    continue;
                }

                break;
            }

            if (_oxygenX > int.MinValue)
            {
                if (Math.Abs(x - _oxygenX) == 1 && y == _oxygenY || Math.Abs(y - _oxygenY) == 1 && x == _oxygenX)
                {
                    if (Explored())
                    {
                        break;
                    }
                }
            }
        }

        ConvertToArray();
    }

    private void ConvertToArray()
    {
        var xMin = _map.Min(m => m.Key);

        var xMax = _map.Max(m => m.Key);

        var yMin = _map.SelectMany(m => m.Value.Keys).Min();

        var yMax = _map.SelectMany(m => m.Value.Keys).Max();

        Width = xMax - xMin + 1;

        Height = yMax - yMin + 1;

        var result = new bool[Width, Height];

        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                result[x, y] = GetCellType(x + xMin, y + yMin) != CellType.Wall && GetCellType(x + xMin, y + yMin) != CellType.Unknown;
            }
        }

        Map = result;

        Origin = new Point(0 - xMin, 0 - yMin);

        Destination = new Point(_oxygenX - xMin, _oxygenY - yMin);
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

        var best = neighbors.MinBy(n => (int) n.CellType);

        if (neighbors.Count(n => n.CellType == CellType.Wall) == 3)
        {
            SetCellType(x, y, CellType.DeadEnd);
        }

        return best;
    }

    private CellType GetCellType(int x, int y)
    {
        if (! _map.TryGetValue(x, out var value))
        {
            return CellType.Unknown;
        }

        if (! value.ContainsKey(y))
        {
            return CellType.Unknown;
        }

        return _map[x][y];
    }

    private void SetCellType(int x, int y, CellType cellType)
    {
        if (! _map.TryGetValue(x, out var value))
        {
            value = new Dictionary<int, CellType>();
            
            _map.Add(x, value);
        }

        if (value.TryAdd(y, cellType))
        {
            return;
        }

        value[y] = cellType;
    }
}