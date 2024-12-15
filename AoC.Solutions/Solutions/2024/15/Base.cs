using System.Text;
using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._15;

public abstract class Base : Solution
{
    public override string Description => "Warehouse woes";

    protected bool IsPart2;
    
    private char[,] _map;

    private int _width;

    private int _height;

    private string _directions;

    private int _robotX;

    private int _robotY;

    protected void RunRobot()
    {
        for (var i = 0; i < _directions.Length; i++)
        {
            var (dX, dY) = _directions[i] switch
            {
                '^' => (0, -1),
                'v' => (0, 1),
                '<' => (-1, 0),
                _ => (1, 0)
            };

            if (MakeMove(_robotX, _robotY, dX, dY))
            {
                _robotX += dX;

                _robotY += dY;
            }
        }
        
        DumpMap();
    }

    private bool MakeMove(int x, int y, int dX, int dY)
    {
        x += dX;

        y += dY;

        var cell = _map[x, y];
        
        if (cell == '#')
        {
            return false;
        }
        
        if (cell is 'O' or '@')
        { 
            MakeMove(x, y, dX, dY);
        }
        
        if (_map[x, y] == '.')
        {
            _map[x, y] = _map[x - dX, y - dY];
            _map[x - dX, y - dY] = '.';

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

    private void DumpMap()
    {
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                // if (x == _robotX && y == _robotY)
                // {
                //     Console.Write('@');
                //     
                //     continue;
                // }

                Console.Write(_map[x, y]);
            }
            
            Console.WriteLine();
        }
        
        Console.WriteLine();
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
                    var character = line[x];
                    
                    switch (character)
                    {
                        case '.':
                        case '@':
                            _map[x * 2, y] = '.';
                            _map[x * 2 + 1, y] = '.';
                            break;
                        
                        default:
                            _map[x * 2, y] = character;
                            _map[x * 2 + 1, y] = character;
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

            // _map[_robotX, _robotY] = '.';
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