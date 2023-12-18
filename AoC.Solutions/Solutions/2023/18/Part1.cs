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

        var trench = DigTrench();

        CreateMap(trench);        
        
        FloodFill(0, 0);
        
        Dump();

        var result = CountArea();
        
        return result.ToString();
    }

    private void Dump()
    {
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                Console.Write(_map[x, y] == '\0' ? '.' : _map[x, y]);
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

    private void CreateMap(List<(int X, int Y)> points)
    {
        var minX = points.Min(p => p.X);
        
        var maxX = points.Max(p => p.X);

        var minY = points.Min(p => p.Y);
        
        var maxY = points.Max(p => p.Y);

        _width = maxX - minX + 3;

        _height = maxY - minY + 3;
        
        _map = new char[_width, _height];

        foreach (var point in points)
        {
            _map[point.X - minX + 1, point.Y - minY + 1] = '#';
        }
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
    
    private List<(int X, int Y)> DigTrench()
    {
        int x = 0, y = 0;

        var trench = new List<(int X, int Y)>();

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

                trench.Add((x, y));
            }
        }
        
        return trench;
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