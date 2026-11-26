using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._20;

public abstract class Base : Solution
{
    public override string Description => "Race condition";

    private readonly IVisualiser<PuzzleState> _visualiser;

    private char[,] _map;

    private int _width;

    private int _height;

    private Point2D _start;

    private Point2D _end;

    protected Base()
    {
    }

    protected Base(IVisualiser<PuzzleState> visualiser)
    {
        _visualiser = visualiser;
    }

    protected int Solve(int cheatTime)
    {
        var state = Race();

        var count = 0;

        var track = new State[state.Steps + 1];

        var i = 0;

        while (state != null)
        {
            track[i] = state;

            state = state.Previous;

            i++;
        }
        
        if (_visualiser == null)
        {
            return CountCheatsFast(track, cheatTime);
        }

        Visualise(track, Point2D.Null, Point2D.Null);
        
        for (i = 0; i < track.Length - 1; i++)
        {
            var left = track[i];

            for (var j = i + 1; j < track.Length; j++)
            {
                var right = track[j];

                var distance = left.Position.ManhattanDistance(right.Position);

                if (distance <= cheatTime)
                {
                    var saving = left.Steps - right.Steps - distance;

                    if (saving < 100)
                    {
                        continue;
                    }

                    count++;

                    Visualise(null, left.Position, right.Position);
                }
            }
        }

        return count;
    }

    private void Visualise(State[] state, Point2D shortcutStart, Point2D shortcutEnd)
    {
        _visualiser?.PuzzleStateChanged(new PuzzleState(_map, state, shortcutStart, shortcutEnd));
    }

    private State Race()
    {
        var queue = new Queue<State>();

        queue.Enqueue(new State(_start, Point2D.South, 0));

        while (queue.Count > 0)
        {
            var state = queue.Dequeue();

            var position = state.Position;

            position += state.Direction;

            var steps = state.Steps;

            if (position == _end)
            {
                return new State(position, Point2D.Null, steps + 1, state);
            }

            if (state.Direction != Point2D.South && _map[position.X, position.Y - 1] != '#')
            {
                queue.Enqueue(new State(position, Point2D.North, steps + 1, state));
            }

            if (state.Direction != Point2D.West && _map[position.X + 1, position.Y] != '#')
            {
                queue.Enqueue(new State(position, Point2D.East, steps + 1, state));
            }

            if (state.Direction != Point2D.North && _map[position.X, position.Y + 1] != '#')
            {
                queue.Enqueue(new State(position, Point2D.South, steps + 1, state));
            }

            if (state.Direction != Point2D.East && _map[position.X - 1, position.Y] != '#')
            {
                queue.Enqueue(new State(position, Point2D.West, steps + 1, state));
            }
        }

        return null;
    }

    protected void ParseInput()
    {
        _width = Input[0].Length;

        _height = Input.Length;

        _map = new char[_width, _height];

        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                var c = Input[y][x];

                switch (c)
                {
                    case '#':
                        _map[x, y] = '#';
                        continue;

                    case 'S':
                        _start = new Point2D(x, y);
                        break;

                    case 'E':
                        _end = new Point2D(x, y);
                        break;
                }

                _map[x, y] = '.';
            }
        }
    }

    private int CountCheatsFast(State[] track, int cheatTime)
    {
        var steps = new int[_width * _height];

        Array.Fill(steps, -1);

        foreach (var state in track)
        {
            steps[state.Position.Y * _width + state.Position.X] = state.Steps;
        }

        var offsets = new List<(int X, int Y, int Distance)>();

        for (var dy = -cheatTime; dy <= cheatTime; dy++)
        {
            var extent = cheatTime - Math.Abs(dy);

            for (var dx = -extent; dx <= extent; dx++)
            {
                var distance = Math.Abs(dx) + Math.Abs(dy);

                if (distance > 0)
                {
                    offsets.Add((dx, dy, distance));
                }
            }
        }

        var count = 0;

        foreach (var state in track)
        {
            if (state.Steps < 100)
            {
                continue;
            }

            foreach (var offset in offsets)
            {
                var x = state.Position.X + offset.X;

                var y = state.Position.Y + offset.Y;

                if ((uint) x >= (uint) _width || (uint) y >= (uint) _height)
                {
                    continue;
                }

                var destination = steps[y * _width + x];

                if (destination >= 0 && state.Steps - destination - offset.Distance >= 100)
                {
                    count++;
                }
            }
        }

        return count;
    }
}