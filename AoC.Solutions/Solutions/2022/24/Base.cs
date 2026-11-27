using AoC.Solutions.Exceptions;
using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._24;

public abstract class Base : Solution
{
    public override string Description => "Blizzard basin";

    private bool[,] _leftStorms;

    private bool[,] _downStorms;

    private bool[,] _rightStorms;

    private bool[,] _upStorms;

    private int _width;

    private int _height;

    private int _blizzardWidth;

    private int _blizzardHeight;

    private int _start;

    private int _end;

    private readonly IVisualiser<PuzzleState> _visualiser;

    protected Base()
    {
    }

    protected Base(IVisualiser<PuzzleState> visualiser)
    {
        _visualiser = visualiser;
    }

    private void Visualise(List<int> moves)
    {
        if (_visualiser != null)
        {
            var state = new PuzzleState
            {
                Map = Input.To2DArray(),
                Moves = [.. moves.Select(m => (m % _width, m / _width))]
            };

            _visualiser.PuzzleStateChanged(state);
        }
    }

    protected void ParseInput()
    {
        _width = Input[0].Length;

        _height = Input.Length;

        _blizzardWidth = _width - 2;

        _blizzardHeight = _height - 2;

        _leftStorms = new bool[_height, _width];

        _downStorms = new bool[_width, _height];

        _rightStorms = new bool[_height, _width];

        _upStorms = new bool[_width, _height];

        for (var y = 0; y < _height; y++)
        {
            var line = Input[y];

            for (var x = 0; x < _width; x++)
            {
                var c = line[x];

                if (y == 0 && c == '.')
                {
                    _start = x + y * _width;
                }

                if (y == Input.Length - 1 && c == '.')
                {
                    _end = x + y * _width;
                }

                switch (c)
                {
                    case '#':
                    case '.':
                        continue;
                    case '<':
                        _leftStorms[y, x] = true;

                        continue;
                    case '>':
                        _rightStorms[y, x] = true;

                        continue;
                    case 'v':
                        _downStorms[x, y] = true;

                        continue;
                    case '^':
                        _upStorms[x, y] = true;
                        break;
                }
            }
        }
    }

    protected int RunSimulation(int loops = 1)
    {
        if (_visualiser == null && _blizzardWidth <= 128)
        {
            return RunSimulationFast(loops);
        }

        var steps = 0;

        for (var i = 0; i < loops; i++)
        {
            steps += RunSimulationStep(steps, i);
        }

        return steps;
    }

    private int RunSimulationFast(int loops)
    {
        var width = _blizzardWidth;

        var height = _blizzardHeight;

        var mask = UInt128.MaxValue >> (128 - width);

        var left = new UInt128[height];

        var right = new UInt128[height];

        var up = new UInt128[height];

        var down = new UInt128[height];

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var bit = UInt128.One << x;

                switch (Input[y + 1][x + 1])
                {
                    case '<':
                        left[y] |= bit;
                        break;

                    case '>':
                        right[y] |= bit;
                        break;

                    case '^':
                        up[y] |= bit;
                        break;

                    case 'v':
                        down[y] |= bit;
                        break;
                }
            }
        }

        var startBit = UInt128.One << (_start % _width - 1);

        var endBit = UInt128.One << (_end % _width - 1);

        var frontier = new UInt128[height + 2];

        var next = new UInt128[height + 2];

        var minute = 0;

        for (var leg = 0; leg < loops; leg++)
        {
            Array.Clear(frontier);

            var reverse = (leg & 1) != 0;

            var origin = reverse ? height + 1 : 0;

            var target = reverse ? 0 : height + 1;

            var targetBit = reverse ? startBit : endBit;

            frontier[origin] = reverse ? endBit : startBit;

            while ((frontier[target] & targetBit) == 0)
            {
                minute++;

                var phase = minute % height;

                next[0] = (frontier[0] | frontier[1]) & startBit;

                next[height + 1] =
                    (frontier[height] | frontier[height + 1]) & endBit;

                for (var y = 0; y < height; y++)
                {
                    var l = left[y];

                    var r = right[y];

                    left[y] = (l >> 1) |
                              ((l & UInt128.One) << (width - 1));

                    right[y] = ((r << 1) & mask) |
                               (r >> (width - 1));

                    var occupied = left[y] | right[y] |
                                   up[(y + phase) % height] |
                                   down[(y + height - phase) % height];

                    var row = y + 1;

                    var current = frontier[row];

                    var reachable = current |
                                    (current << 1) |
                                    (current >> 1) |
                                    frontier[row - 1] |
                                    frontier[row + 1];

                    next[row] = reachable & mask & ~occupied;
                }

                (frontier, next) = (next, frontier);
            }
        }

        return minute;
    }

    private int RunSimulationStep(int startIteration, int loop)
    {
        var queue = new PriorityQueue<(int Position, int Steps, List<int> History), int>();

        var visited = new HashSet<int>();

        var origin = loop == 1 ? _end : _start;

        var target = loop == 1 ? _start : _end;

        queue.Enqueue((origin, 0, _visualiser == null ? null : [origin]), 0);

        while (queue.Count > 0)
        {
            var item = queue.Dequeue();

            if (item.Position == target && item.Position == target)
            {
                Visualise(item.History);

                return item.Steps;
            }

            var moves = GenerateMoves(item.Position, target, origin, startIteration + item.Steps + 1);

            foreach (var move in moves)
            {
                var hash = new HashCode();

                hash.Add(move);
                hash.Add(item.Steps);

                var code = hash.ToHashCode();

                if (! visited.Contains(code))
                {
                    queue.Enqueue((move, item.Steps + 1, _visualiser == null ? null : [.. item.History, move]), Math.Abs(target % _width - move % _width) + Math.Abs(target / _width - move / _width) + item.Steps);

                    visited.Add(code);
                }
            }
        }

        throw new PuzzleException("Solution not found.");
    }

    private List<int> GenerateMoves(int position, int target, int origin, int iteration)
    {
        var moves = new List<int>();

        var x = position % _width;

        var y = position / _width;

        // Reached goal (end).
        if (target == _end && x == target % _width && y == target / _width - 1)
        {
            moves.Add(position + _width);

            return moves;
        }

        // Reached goal (start).
        if (target == _start && x == target % _width && y == target / _width + 1)
        {
            moves.Add(position - _width);

            return moves;
        }

        // Loiter.
        if (! IsOccupied(x, y, iteration))
        {
            moves.Add(position);
        }

        // In and out of start/end.
        if (origin.Equals(_start))
        {
            switch (y)
            {
                case 0 when x == origin % _width:
                    moves.Add(x + _width);

                    return moves;
                case 1 when x == origin % _width:
                    moves.Add(position - _width);
                    break;
            }
        }
        else
        {
            if (y == _height - 1 && x == origin % _width)
            {
                moves.Add(position - _width * 2);

                return moves;
            }

            if (y == _height - 2 && x == origin % _width)
            {
                moves.Add(position + _width);
            }
        }

        // Right?
        if (x < _blizzardWidth && ! IsOccupied(x + 1, y, iteration))
        {
            moves.Add(position + 1);
        }

        // Left?
        if (x > 1 && ! IsOccupied(x - 1, y, iteration))
        {
            moves.Add(position - 1);
        }

        // Down?
        if (y < _blizzardHeight && ! IsOccupied(x, y + 1, iteration))
        {
            moves.Add(position + _width);
        }

        // Up?
        if (y > 1 && ! IsOccupied(x, y - 1, iteration))
        {
            moves.Add(position - _width);
        }

        return moves;
    }

    private bool IsOccupied(int x, int y, int iteration)
    {
        if (x < 1 || y < 1 || x >= _width || y >= _height)
        {
            return false;
        }

        var xD = iteration % _blizzardWidth;

        var target = (x - 1 + _blizzardWidth - xD) % _blizzardWidth + 1;

        var found = _rightStorms[y, target];

        if (found)
        {
            return true;
        }

        target = (x - 1 + xD) % _blizzardWidth + 1;

        found = _leftStorms[y, target];

        if (found)
        {
            return true;
        }

        var yD = iteration % _blizzardHeight;

        target = (y - 1 + _blizzardHeight - yD) % _blizzardHeight + 1;

        found = _downStorms[x, target];

        if (found)
        {
            return true;
        }

        target = (y - 1 + yD) % _blizzardHeight + 1;

        found = _upStorms[x, target];

        return found;
    }
}