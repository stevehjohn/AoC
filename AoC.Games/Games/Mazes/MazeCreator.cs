using AoC.Solutions.Extensions;

namespace AoC.Games.Games.Mazes;

public class MazeCreator
{
    private readonly Stack<(int X, int Y)> _stack = [];

    private readonly HashSet<(int X, int Y)> _visited = [];

    private (int X, int Y) _position;

    private (int Dx, int Dy) _direction;

    private readonly Random _rng = new();

    private int _nextRandom = -1;

    private int _move;

    private readonly bool[,] _maze;

    public MazeCreator(bool[,] maze)
    {
        _maze = maze;
    }

    public void Reset()
    {
        _position = (_rng.Next((Constants.Width - 2) / 2) * 2 + 1, _rng.Next((Constants.Height - 2) / 2) * 2 + 1);

        var direction = _rng.Next(2) == 0 ? 1 : -1;
        
        _direction = _rng.Next(2) == 0 ? (0, direction) : (direction, 0);

        if (_position.X + _direction.Dx is 0 or Constants.Width)
        {
            _direction.Dx = -_direction.Dx;
        }

        if (_position.Y + _direction.Dy is 0 or Constants.Width)
        {
            _direction.Dy = -_direction.Dy;
        }

        _move = 0;

        _maze.ForAll((x, y, _) => _maze[x, y] = false);
        
        _visited.Clear();
        
        _stack.Clear();
    }

    public bool CreateMaze()
    {
        if (_position.Y >= 0)
        {
            _stack.Push(_position);

            _visited.Add(_position);

            _maze[_position.X, _position.Y] = true;
        }

        if (_move > 0)
        {
            var directions = GetDirections();

            while (directions.Count == 0)
            {
                if (! _stack.TryPop(out var position))
                {
                    AddStartAndEnd();
                    
                    return true;
                }
                
                _position = position;

                directions = GetDirections();
            }

            if (_nextRandom == -1 || _move % Constants.StraightLength == 0 || _nextRandom >= directions.Count)
            {
                _nextRandom = _rng.Next(directions.Count);
            }

            _direction = directions[_nextRandom];
        }

        _position.X += _direction.Dx;
        _position.Y += _direction.Dy;

        _maze[_position.X, _position.Y] = true;

        _position.X += _direction.Dx;
        _position.Y += _direction.Dy;

        _move++;

        return false;
    }

    private void AddStartAndEnd()
    {
        var x = 0;

        var y = 0;
        
        if (_rng.Next(2) == 0)
        {
            x = _rng.Next((Constants.Width - 2) / 2) * 2 + 1;
        }
        else
        {
            y = _rng.Next((Constants.Height - 2) / 2) * 2 + 1;
        }

        _maze[x, y] = true;

        var solver = new MazeSolver(_maze);

        var exit = solver.FindFurthestEdgeFrom(x, y);

        switch (exit.X)
        {
            case 2:
                exit.X = 0;
                break;
            case Constants.Width - 3:
                exit.X = Constants.Width - 1;
                break;
            default:
            {
                if (exit.Y == 2)
                {
                    exit.Y = 0;
                }
                else
                {
                    exit.Y = Constants.Height - 1;
                }

                break;
            }
        }

        _maze[exit.X, exit.Y] = true;
    }

    private List<(int Dx, int Dy)> GetDirections()
    {
        var directions = new List<(int Dx, int Dy)>();
        
        if (_position.X > 2 && ! _visited.Contains((_position.X - 2, _position.Y)))
        {
            directions.Add((-1, 0));
        }
        
        if (_position.Y < Constants.Height - 3 && ! _visited.Contains((_position.X, _position.Y + 2)))
        {
            directions.Add((0, 1));
        }

        if (_position.X < Constants.Width - 3 && ! _visited.Contains((_position.X + 2, _position.Y)))
        {
            directions.Add((1, 0));
        }

        if (_position.Y > 2 && ! _visited.Contains((_position.X, _position.Y - 2)))
        {
            directions.Add((0, -1));
        }

        return directions;
    }
}