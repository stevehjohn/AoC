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
            MakeMove(_directions[i]);
        }
            
        DumpMap();
    }

    private void MakeMove(char direction)
    {
        var (dX, dY) = direction switch
        {
            '^' => (0, -1),
            'v' => (0, 1),
            '<' => (-1, 0),
            _ => (1, 0)
        };

        var x = _robotX + dX;

        var y = _robotY + dY;

        var cell = _map[x, y];

        switch (cell)
        {
            case '#':
                return;

            case '.':
                _robotX = x;

                _robotY = y;

                return;
        }

        var i = 1;

        while (_map[x + i * dX, y + i * dY] is 'O' and not '#')
        {
            i++;
        }

        if (_map[x + i * dX, y + i * dY] == '#')
        {
            return;
        }

        for (; i > 0; i--)
        {
            _map[x + i * dX, y + i * dY] = _map[x + (i - 1) * dX, y + (i - 1) * dY];

            _map[x + (i - 1) * dX, y + (i - 1) * dY] = '.';
        }

        _robotX = x;

        _robotY = y;
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
                if (x == _robotX && y == _robotY)
                {
                    Console.Write('@');
                    
                    continue;
                }

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

        _map = Input[..y].To2DArray();

        _map[(int) _robotX, _robotY] = '.';
        
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