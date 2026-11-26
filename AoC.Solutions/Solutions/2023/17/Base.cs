using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._17;

public abstract class Base : Solution
{
    public override string Description => "Clumsy crucible";

    private int[,] _map;

    private int _width;

    private int _height;

    private static readonly (int, int) North = (0, -1);

    private static readonly (int, int) East = (1, 0);

    private static readonly (int, int) South = (0, 1);

    private static readonly (int, int) West = (-1, 0);

    private readonly IVisualiser<PuzzleState> _visualiser;

    protected Base()
    {
    }

    protected Base(IVisualiser<PuzzleState> visualiser)
    {
        _visualiser = visualiser;
    }

    private void Visualise(List<(int X, int Y)> history = null)
    {
        _visualiser?.PuzzleStateChanged(new PuzzleState { Map = _map, History = history?.ToList() });
    }

    protected int Solve(int minSteps, int maxSteps)
    {
        if (_visualiser == null)
        {
            return SolveFast(Math.Max(1, minSteps), maxSteps);
        }

        Visualise();

        var queue = new PriorityQueue<(int X, int Y, (int Dx, int Dy) Direction, int Steps, List<(int X, int Y)> History), int>();

        var visited = new bool[_width, _height, 4, 10];

        queue.Enqueue((0, 0, East, 1, [(0, 0)]), 0);
        queue.Enqueue((0, 0, South, 1, [(0, 0)]), 0);

        var directions = new List<(int Dx, int Dy)>();

        while (queue.TryDequeue(out var item, out var cost))
        {
            if (item.X == _width - 1 && item.Y == _height - 1 && item.Steps >= minSteps - 1)
            {
                return cost;
            }

            Visualise(item.History);

            directions.Clear();

            if (item.Steps < minSteps - 1)
            {
                directions.Add(item.Direction);
            }
            else
            {
                GetDirections(directions, item.Direction);
            }

            foreach (var direction in directions)
            {
                var newSteps = direction == item.Direction ? item.Steps + 1 : 0;

                if (newSteps == maxSteps)
                {
                    continue;
                }

                var (x, y) = (item.X + direction.Dx, item.Y + direction.Dy);

                if (x < 0 || x == _width || y < 0 || y == _height)
                {
                    continue;
                }

                var i = item.Direction switch
                {
                    (0, -1) => 0,
                    (1, 0) => 1,
                    (0, 1) => 2,
                    _ => 3
                };

                if (! visited[x, y, i, newSteps])
                {
                    if (_visualiser != null)
                    {
                        queue.Enqueue((x, y, direction, newSteps, [.. item.History, (x, y)]), cost + _map[x, y]);
                    }
                    else
                    {
                        queue.Enqueue((x, y, direction, newSteps, null), cost + _map[x, y]);
                    }

                    visited[x, y, i, newSteps] = true;
                }
            }
        }

        return 0;
    }
    
    private int SolveFast(int minSteps, int maxSteps)
    {
        var width = _width;

        var height = _height;

        var size = width * height;

        var map = new int[size];

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                map[y * width + x] = _map[x, y];
            }
        }

        var distances = new int[size * 2];

        Array.Fill(distances, int.MaxValue);

        var queue = new PriorityQueue<int, int>();

        distances[0] = 0;
        distances[1] = 0;

        queue.Enqueue(0, 0);
        queue.Enqueue(1, 0);

        while (queue.TryDequeue(out var state, out var cost))
        {
            if (cost != distances[state])
            {
                continue;
            }

            var position = state >> 1;

            if (position == size - 1)
            {
                return cost;
            }

            var x = position % width;

            var y = position / width;

            var axis = state & 1;

            if (axis == 0)
            {
                Explore(x, y, -1, 0, cost, 1);
                Explore(x, y, 1, 0, cost, 1);
            }
            else
            {
                Explore(x, y, 0, -1, cost, 0);
                Explore(x, y, 0, 1, cost, 0);
            }
        }

        throw new InvalidOperationException("No route found.");

        void Explore(int x, int y, int dx, int dy,
            int cost, int nextAxis)
        {
            for (var step = 1; step <= maxSteps; step++)
            {
                x += dx;
                y += dy;

                if ((uint) x >= (uint) width ||
                    (uint) y >= (uint) height)
                {
                    break;
                }

                var position = y * width + x;

                cost += map[position];

                if (step < minSteps)
                {
                    continue;
                }

                var state = (position << 1) | nextAxis;

                if (cost >= distances[state])
                {
                    continue;
                }

                distances[state] = cost;

                queue.Enqueue(state, cost);
            }
        }
    }

    private static void GetDirections(List<(int, int)> directions, (int, int) direction)
    {
        if (direction != South)
        {
            directions.Add(North);
        }

        if (direction != West)
        {
            directions.Add(East);
        }

        if (direction != North)
        {
            directions.Add(South);
        }

        if (direction != East)
        {
            directions.Add(West);
        }
    }

    protected void ParseInput()
    {
        _width = Input[0].Length;

        _height = Input.Length;

        _map = new int[_width, _height];

        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                _map[x, y] = Input[y][x] - '0';
            }
        }
    }
}