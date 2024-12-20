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

        var track = new State[state.Steps];

        var i = 0;
        
        while (state != null)
        {
            track[i] = state;
            
            state = state.Previous;

            i++;
        }

        Visualise(track);
        
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
                }
            }
        }

        return count;
    }

    private void Visualise(State[] state)
    {
        _visualiser.PuzzleStateChanged(new PuzzleState(_map, state));
    }

    private State Race()
    {
        var queue = new PriorityQueue<State, int>();

        var visited = new HashSet<Point2D>();
        
        queue.Enqueue(new State(_start, Point2D.North,  1), 1);
        queue.Enqueue(new State(_start, Point2D.East,  1), 1);
        queue.Enqueue(new State(_start, Point2D.South,  1), 1);
        queue.Enqueue(new State(_start, Point2D.West,  1), 1);

        visited.Add(_start);
        
        while (queue.Count > 0)
        {
            var state = queue.Dequeue();

            var position = state.Position;

            position += state.Direction;

            if (_map[position.X, position.Y] == '#')
            {
                continue;
            }

            if (! visited.Add(position))
            {
                continue;
            }

            var steps = state.Steps;
            
            if (position == _end)
            {
                return new State(position, Point2D.Null, steps + 1, state);
            }

            queue.Enqueue(new State(position, Point2D.North, steps + 1, state), steps + 1);
            queue.Enqueue(new State(position, Point2D.East, steps + 1, state), steps + 1);
            queue.Enqueue(new State(position, Point2D.South, steps + 1, state), steps + 1);
            queue.Enqueue(new State(position, Point2D.West, steps + 1, state), steps + 1);
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
}