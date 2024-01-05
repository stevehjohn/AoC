using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2018._15;

public abstract class Base : Solution
{
    public override string Description => "Elves vs Goblins";

    private int _width;

    private int _height;

    private bool[,] _map;

    private readonly List<Unit> _units = [];

    protected void Reset()
    {
        _units.Clear();
    }

    protected (bool NoElfLosses, int Outcome) Play(int elfPower = 3)
    {
        var round = 0;

        var elves = _units.Count(u => u.Type == Type.Elf);

        _units.ForEach(u => u.Power = u.Type == Type.Elf ? elfPower : 3);

        while (true)
        {
            var unitOrder = _units.OrderBy(u => u.Position.Y).ThenBy(u => u.Position.X).ToList();

            var i = 0;

            while (i < unitOrder.Count)
            {
                var removed = unitOrder[i].Play();

                if (removed != null && unitOrder.IndexOf(removed) > i)
                {
                    unitOrder.Remove(removed);
                }

                if (_units.DistinctBy(u => u.Type).Count() == 1)
                {
                    if (i == unitOrder.Count - 1)
                    {
                        round++;
                    }

                    goto escape;
                }

                i++;
            }

            round++;
        }

        escape:

        return (_units.Count(u => u.Type == Type.Elf) == elves, round * _units.Sum(u => u.Health));
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

                switch (c)
                {
                    case '.':
                        continue;
                    case '#':
                        _map[x, y] = true;

                        continue;
                    default:
                        _units.Add(new Unit(c == 'E' ? Type.Elf : Type.Goblin, new Point(x, y), _map, _units));
                        break;
                }
            }
        }
    }
}