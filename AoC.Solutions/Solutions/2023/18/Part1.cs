using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._18;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly List<(char Direction, int Steps)> _moves = new();

    private int _width;

    private int _height;
    
    private char[,] _map;
    
    public override string GetAnswer()
    {
        ParseInput();

        _width = 9;

        _height = 12;
        
        _map = new char[_width, _height];

        DigTrench();
        
        FloodFill(0, 0);

        var result = CountArea();
        
        Dump();
        
        return result.ToString();
    }

    private void Dump()
    {
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                Console.Write(_map[x, y] == '\0' ? ' ' : _map[x, y]);
            }
            
            Console.WriteLine();
        }
    }

    private int CountArea()
    {
        var area = 0;
        
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                area += _map[x, y] is '\0' or '#' ? 1 : 0;
            }
        }

        return area;
    }

    private void FloodFill(int x, int y)
    {
        var queue = new Queue<(int X, int Y)>();
        
        queue.Enqueue((x, y));

        while (queue.Count > 0)
        {
            (x, y) = queue.Dequeue();
            
            _map[x, y] = '*';
            
            if (x > 0 && _map[x - 1, y] == '\0')
            {
                _map[x - 1, y] = '*';
                
                queue.Enqueue((x - 1, y));
            }

            if (x < _width - 1 && _map[x + 1, y] == '\0')
            {
                _map[x + 1, y] = '*';
                
                queue.Enqueue((x + 1, y));
            }

            if (y < _height - 1 && _map[x, y + 1] == '\0')
            {
                _map[x, y + 1] = '*';
                
                queue.Enqueue((x, y + 1));
            }

            if (y > 0 && _map[x, y - 1] == '\0')
            {
                _map[x, y - 1] = '*';
                
                queue.Enqueue((x, y - 1));
            }
        }
    }
    
    private void DigTrench()
    {
        int x = 1, y = 1;

        _map[x, y] = '#';

        foreach (var move in _moves)
        {
            var dX = move.Direction switch
            {
                'L' => -1,
                'R' => 1,
                _ => 0
            };
            
            var dY = move.Direction switch
            {
                'U' => -1,
                'D' => 1,
                _ => 0
            };

            for (var i = 0; i < move.Steps; i++)
            {
                x += dX;
                y += dY;

                _map[x, y] = '#';
            }
        }
    }

    private void ParseInput()
    {
        foreach (var line in Input)
        {
            var parts = line.Split(' ');
            
            _moves.Add((parts[0][0], int.Parse(parts[1])));
        }
    }
}