using AoC.Solutions.Extensions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._10;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly HashSet<(int, int)> _trailheads = [];

    private readonly Queue<(int X, int Y)> _queue = [];
    
    private char[,] _map;

    private int _width;

    private int _height;

    public override string GetAnswer()
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
        _trailheads.Clear();
        
        _queue.Enqueue((x, y));

        while (_queue.Count > 0)
        {
            var position = _queue.Dequeue();

            if (_map[position.X, position.Y] == '9')
            {
                _trailheads.Add((position.X, position.Y));
                
                continue;
            }

            var height = _map[x, y];
            
            SafeEnqueueValue(1 - 1, y, height);
            
            SafeEnqueueValue(1 + 1, y, height);
            
            SafeEnqueueValue(1, y - 1, height);
            
            SafeEnqueueValue(1, y + 1, height);
        }

        return _trailheads.Count;
    }

    private void SafeEnqueueValue(int x, int y, char height)
    {
        if (x < 0 || x >= _width || y < 0 || y >= _height)
        {
            return;
        }

        if (_map[x, y] == height + 1)
        {
            _queue.Enqueue((x, y));
        }
    }
}