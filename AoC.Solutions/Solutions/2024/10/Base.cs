using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._10;

public abstract class Base : Solution
{
    public override string Description => "Hoof it";
    
    private readonly HashSet<(int, int)> _trailEnds = [];

    private readonly HashSet<(int, int)> _visited = [];

    private readonly Queue<(int X, int Y)> _queue = [];
    
    private char[,] _map;

    private int _width;

    private int _height;

    protected string GetAnswer(bool isPart2)
    {
        _map = Input.To2DArray();

        _width = _map.GetUpperBound(0);

        _height = _map.GetUpperBound(1);
        
        var result = 0;
        
        _map.ForAll((x, y, c) =>
        {
            if (c == '0')
            {
                result += CountTrailheads(x, y);
            }
        });
        
        return result.ToString();
    }

    private int CountTrailheads(int x, int y)
    {
        _trailEnds.Clear();
        
        _visited.Clear();
        
        _queue.Enqueue((x, y));

        while (_queue.Count > 0)
        {
            var position = _queue.Dequeue();

            if (_map[position.X, position.Y] == '9')
            {
                _trailEnds.Add((position.X, position.Y));
                
                continue;
            }

            var height = _map[position.X, position.Y];
            
            SafeEnqueueValue(position.X - 1, position.Y, height);
            
            SafeEnqueueValue(position.X + 1, position.Y, height);
            
            SafeEnqueueValue(position.X, position.Y - 1, height);
            
            SafeEnqueueValue(position.X, position.Y + 1, height);
        }

        return _trailEnds.Count;
    }

    private void SafeEnqueueValue(int x, int y, char height)
    {
        if (x < 0 || x > _width || y < 0 || y > _height)
        {
            return;
        }

        if (_map[x, y] == height + 1 && _visited.Add((x, y)))
        {
            _queue.Enqueue((x, y));
        }
    }
}