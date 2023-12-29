using AoC.Solutions.Extensions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._21;

[UsedImplicitly]
public class Part2 : Base
{
    private const long TargetSteps = 26_501_365;

    private const int Buffers = 4;
    
    private long[] _counts;
    
    private readonly HashSet<(int X, int Y, int Ux, int Uy)>[] _buffers = new HashSet<(int X, int Y, int Ux, int Uy)>[Buffers];

    private int _source;

    private int _target = Buffers - 1;
    
    private static readonly (int, int) North = (0, -1);
    
    private static readonly (int, int) East = (1, 0);
    
    private static readonly (int, int) South = (0, 1);
    
    private static readonly (int, int) West = (-1, 0);
    
    public override string GetAnswer()
    {
        var start = ParseInput();

        Walk(start, Width * 2 + Width / 2 + 1);

        var result = ExtrapolateAnswer();
        
        return result.ToString();
    }

    private void Walk((int X, int Y) start, int maxSteps)
    {
        for (var i = 0; i < Buffers; i++)
        {
            _buffers[i] = new HashSet<(int X, int Y, int Ux, int Uy)>();
        }

        _buffers[_source].Add((start.X, start.Y, 0, 0));
        
        _counts = new long[maxSteps];

        _counts[0] = 1;
        
        var step = 1;
        
        while (step < maxSteps)
        {
            var count = 0;

            _buffers[_source].ExceptWith(_buffers[_source.DecRotate(Buffers - 1).DecRotate(Buffers - 1)]);
            
            foreach (var position in _buffers[_source])
            {
                count += Move(position, North);
            
                count += Move(position, South);
            
                count += Move(position, East);
            
                count += Move(position, West);
            }

            _counts[step] = count;

            if (step > 3)
            {
                _counts[step] += _counts[step - 4];
            }

            // Console.WriteLine($"Step: {step}    Count: {_counts[step]}   Delta: {count}");

            _source = _source.DecRotate(Buffers - 1);

            _target = _target.DecRotate(Buffers - 1);

            _buffers[_target].Clear();
            
            step++;
        }
    }
    
    private int Move((int X, int Y, int Ux, int Uy) position, (int X, int Y) direction)
    {
        position = (position.X + direction.X, position.Y + direction.Y, position.Ux, position.Uy);

        if (position.X < 0)
        {
            position.X = Width - 1;
            position.Ux--;
        }

        if (position.X == Width)
        {
            position.X = 0;
            position.Ux++;
        }

        if (position.Y < 0)
        {
            position.Y = Height - 1;
            position.Uy--;
        }

        if (position.Y == Height)
        {
            position.Y = 0;
            position.Uy++;
        }

        if (Map[position.X, position.Y] == '#')
        {
            return 0;
        }

        return _buffers[_target].Add(position) ? 1 : 0;
    }
    
    private long ExtrapolateAnswer()
    {
        var halfWidth = Width / 2;
        
        var delta1 = _counts[halfWidth + Width] - _counts[halfWidth];

        var delta2 = _counts[halfWidth + Width * 2] - _counts[halfWidth + Width];

        var quotient = TargetSteps / Width;
        
        var result = _counts[halfWidth] + delta1 * quotient + quotient * (quotient - 1) / 2 * (delta2 - delta1);
        
        return result;
    }
}