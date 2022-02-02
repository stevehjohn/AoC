using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2018._15;

public abstract class Base : Solution
{
    public override string Description => "Elves vs Goblins";

    private int _width;

    private int _height;

    private bool[,] _map;

    private readonly List<Unit> _units = new();

    protected int Play()
    {
        var round = 0;

        while (true)
        {
            var unitOrder = _units.OrderBy(u => u.Position.Y).ThenBy(u => u.Position.X);

            foreach (var unit in unitOrder)
            {
                unit.Play();
            }

            Dump();

            if (_units.DistinctBy(u => u.Type).Count() == 1)
            {
                break;
            }

            round++;
        }

        return round * _units.Sum(u => u.Health);
    }

    private void Dump()
    {
        Console.SetCursorPosition(0, 1);

        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                var unit = _units.SingleOrDefault(u => u.Position.X == x && u.Position.Y == y);

                if (unit != null)
                {
                    Console.Write(unit.Type == Type.Elf ? 'E' : 'G');

                    continue;
                }

                Console.Write(_map[x, y] ? '#' : ' ');
            }

            Console.WriteLine();
        }

        //Thread.Sleep(500);
    }

    protected void ParseInput()
    {
        _width = Input[0].Length;

        _height = Input.Length;

        _map = new bool[_width, _height];

        for (var x = 0; x < _width; x++)
        {
            for (var y = 0; y < _height; y++)
            {
                var c = Input[y][x];

                if (c == '.')
                {
                    continue;
                }

                if (c == '#')
                {
                    _map[x, y] = true;

                    continue;
                }

                _units.Add(new Unit(c == 'E' ? Type.Elf : Type.Goblin, new Point(x, y), _map, _units));
            }
        }
    }
}