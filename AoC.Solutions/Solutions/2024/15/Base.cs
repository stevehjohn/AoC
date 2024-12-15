using System.Text;
using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._15;

public abstract class Base : Solution
{
    public override string Description => "Warehouse woes";

    protected bool IsPart2;

    private readonly IVisualiser<PuzzleState> _visualiser;
    
    private char[,] _map;

    private int _width;

    private int _height;

    private string _directions;

    private int _robotX;

    private int _robotY;

    protected Base()
    {
    }

    protected Base(IVisualiser<PuzzleState> visualiser)
    {
        _visualiser = visualiser;
    }
    
    protected void RunRobot()
    {
        Visualise();
        
        for (var i = 0; i < _directions.Length; i++)
        {
            var (dX, dY) = _directions[i] switch
            {
                '^' => (0, -1),
                'v' => (0, 1),
                '<' => (-1, 0),
                _ => (1, 0)
            };
            
            if (MakeMove(_map, _robotX, _robotY, dX, dY))
            {
                _robotX += dX;

                _robotY += dY;
            
                Visualise();
            }
        }
    }

    private void Visualise()
    {
        _visualiser?.PuzzleStateChanged(new PuzzleState(_map));
    }

    private bool MakeMove(char[,] map, int x, int y, int dX, int dY)
    {
        x += dX;

        y += dY;

        var cell = map[x, y];
        
        switch (cell)
        {
            case '#':
                return false;
            
            case 'O' or '@':
            case '[' or ']' when dY == 0:
                MakeMove(map, x, y, dX, dY);
                break;
            
            case '[' or ']':
            {
                var copy = new char[_width, _height];
            
                Buffer.BlockCopy(map, 0, copy, 0, sizeof(char) * _width * _height);

                var sX = cell == '[' ? 1 : -1;

                if (MakeMove(copy, x + sX, y, dX, dY) && MakeMove(copy, x, y, dX, dY))
                {
                    MakeMove(map, x + sX, y, dX, dY);

                    MakeMove(map, x, y, dX, dY);
                }

                break;
            }
        }

        if (map[x, y] == '.')
        {
            map[x, y] = map[x - dX, y - dY];
            map[x - dX, y - dY] = '.';

            return true;
        }

        return false;
    }

    protected long SumCoordinates()
    {
        var sum = 0L;
        
        for (var x = 0; x < _width; x++)
        {
            for (var y = 0; y < _height; y++)
            {
                if (_map[x, y] is 'O' or '[')
                {
                    sum += x + y * 100;
                }
            }
        }

        return sum;
    }

    protected void ParseInput()
    {
        _width = Input[0].Length;
        
        int y;
        
        for (y = 0; y < Input.Length; y++)
        {
            var line = Input[y];

            if (_robotX == 0)
            {
                for (var x = 0; x < _width; x++)
                {
                    if (line[x] == '@')
                    {
                        _robotX = x;

                        _robotY = y;
                        
                        break;
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(Input[y]))
            {
                break;
            }
        }

        _height = y;

        if (IsPart2)
        {
            _map = new char[_width * 2, _height];
            
            for (y = 0; y < _height; y++)
            {
                var line = Input[y];
                
                for (var x = 0; x < _width; x++)
                {
                    switch (line[x])
                    {
                        case 'O':
                            _map[x * 2, y] = '[';
                            _map[x * 2 + 1, y] = ']';
                            break;
                        
                        case '#':
                            _map[x * 2, y] = '#';
                            _map[x * 2 + 1, y] = '#';
                            break;
                        
                        case '@':
                            _map[x * 2, y] = '@';
                            _map[x * 2 + 1, y] = '.';
                            break;

                        default:
                            _map[x * 2, y] = '.';
                            _map[x * 2 + 1, y] = '.';
                            break;
                    }
                }
            }

            _width *= 2;

            _robotX *= 2;
        }
        else
        {
            _map = Input[..y].To2DArray();
        }
        
        y++;

        var builder = new StringBuilder();
        
        while (y < Input.Length)
        {
            builder.Append(Input[y]);

            y++;
        }

        _directions = builder.ToString();
    }
}