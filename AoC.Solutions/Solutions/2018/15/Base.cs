using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2018._15;

public abstract class Base : Solution
{
    public override string Description => "Elves vs Goblins";

    private int _width;

    private int _height;

    private bool[] _walls;

    private readonly List<Unit> _initial = [];

    protected void Reset()
    {
        _initial.Clear();
    }

    protected void ParseInput()
    {
        _width = Input[0].Length;

        _height = Input.Length;

        _walls = new bool[_width * _height];

        _initial.Clear();

        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                var position = y * _width + x;

                switch (Input[y][x])
                {
                    case '#':
                        _walls[position] = true;

                        break;

                    case 'E':
                    case 'G':
                        _initial.Add(new Unit
                        {
                            Position = position,
                            Elf = Input[y][x] == 'E'
                        });

                        break;
                }
            }
        }
    }

    protected (bool NoElfLosses, int Outcome) Play(int elfPower = 3)
    {
        var units = _initial.Select(u => new Unit
        {
            Position = u.Position,
            Elf = u.Elf,
            Power = u.Elf ? elfPower : 3
        }).ToArray();

        var size = _width * _height;

        var occupied = new int[size];

        Array.Fill(occupied, -1);

        var elves = 0;

        var goblins = 0;

        for (var i = 0; i < units.Length; i++)
        {
            occupied[units[i].Position] = i;

            if (units[i].Elf)
            {
                elves++;
            }
            else
            {
                goblins++;
            }
        }

        var visited = new int[size];

        var distance = new int[size];

        var firstStep = new int[size];

        var targets = new int[size];

        var queue = new int[size];

        var generation = 0;

        var rounds = 0;

        var directions = new[]
        {
            -_width, -1, 1, _width
        };

        while (true)
        {
            var order = Enumerable.Range(0, units.Length)
                .Where(i => units[i].Health > 0)
                .OrderBy(i => units[i].Position)
                .ToArray();

            foreach (var id in order)
            {
                var unit = units[id];

                if (unit.Health <= 0)
                {
                    continue;
                }

                if (unit.Elf ? goblins == 0 : elves == 0)
                {
                    return (elves == _initial.Count(u => u.Elf), rounds * units.Sum(u => Math.Max(0, u.Health)));
                }

                var target = FindAdjacentEnemy(unit);

                if (target < 0)
                {
                    var step = FindMove(unit);

                    if (step >= 0)
                    {
                        occupied[unit.Position] = -1;

                        unit.Position = step;

                        occupied[step] = id;
                    }

                    target = FindAdjacentEnemy(unit);
                }

                if (target < 0)
                {
                    continue;
                }

                var enemy = units[target];

                enemy.Health -= unit.Power;

                if (enemy.Health > 0)
                {
                    continue;
                }

                occupied[enemy.Position] = -1;

                if (enemy.Elf)
                {
                    elves--;
                }
                else
                {
                    goblins--;
                }
            }

            rounds++;
        }

        int FindAdjacentEnemy(Unit unit)
        {
            var result = -1;

            var bestHealth = int.MaxValue;

            var bestPosition = int.MaxValue;

            foreach (var direction in directions)
            {
                var position = unit.Position + direction;

                if (! IsValidNeighbour(unit.Position, position))
                {
                    continue;
                }

                var id = occupied[position];

                if (id < 0)
                {
                    continue;
                }

                var enemy = units[id];

                if (enemy.Elf == unit.Elf || enemy.Health <= 0)
                {
                    continue;
                }

                if (enemy.Health < bestHealth || enemy.Health == bestHealth && position < bestPosition)
                {
                    result = id;
                    bestHealth = enemy.Health;
                    bestPosition = position;
                }
            }

            return result;
        }

        int FindMove(Unit unit)
        {
            generation++;

            var targetGeneration = generation;

            foreach (var enemy in units)
            {
                if (enemy.Health <= 0 || enemy.Elf == unit.Elf)
                {
                    continue;
                }

                foreach (var direction in directions)
                {
                    var position = enemy.Position + direction;

                    if (IsValidNeighbour(enemy.Position, position) && ! _walls[position] && occupied[position] < 0)
                    {
                        targets[position] = targetGeneration;
                    }
                }
            }

            generation++;

            var visitGeneration = generation;

            var head = 0;

            var tail = 0;

            queue[tail++] = unit.Position;

            visited[unit.Position] = visitGeneration;

            distance[unit.Position] = 0;

            var bestDestination = -1;

            var bestDistance = int.MaxValue;

            while (head < tail)
            {
                var current = queue[head++];

                var currentDistance = distance[current];

                if (currentDistance > bestDistance)
                {
                    break;
                }

                if (targets[current] == targetGeneration)
                {
                    if (current < bestDestination || bestDestination < 0)
                    {
                        bestDestination = current;
                    }

                    bestDistance = currentDistance;

                    continue;
                }

                if (currentDistance >= bestDistance)
                {
                    continue;
                }

                foreach (var direction in directions)
                {
                    var next = current + direction;

                    if (! IsValidNeighbour(current, next) || _walls[next] || occupied[next] >= 0 || visited[next] == visitGeneration)
                    {
                        continue;
                    }

                    visited[next] = visitGeneration;

                    distance[next] = currentDistance + 1;

                    firstStep[next] = current == unit.Position
                        ? next
                        : firstStep[current];

                    queue[tail++] = next;
                }
            }

            return bestDestination < 0
                ? -1
                : firstStep[bestDestination];
        }
    }

    private bool IsValidNeighbour(int from, int to)
    {
        if ((uint) to >= (uint) (_width * _height))
        {
            return false;
        }

        var difference = to - from;

        if (difference == -_width || difference == _width)
        {
            return true;
        }

        if (difference is -1 or 1)
        {
            return from / _width == to / _width;
        }

        return false;
    }
}