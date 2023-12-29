using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._21;

[UsedImplicitly]
public class Part2 : Base
{
    private const long TargetSteps = 26_501_365;

    private long[] _counts;

    private HashSet<(int X, int Y, int Ux, int Uy)> _sourcePositions = new();

    private HashSet<(int X, int Y, int Ux, int Uy)> _targetPositions = new();

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
        _sourcePositions.Add((start.X, start.Y, 0, 0));
        
        _counts = new long[maxSteps];

        var step = 1;
        
        /*
         * TODO: Just move out from the center tracking new points on the frontier.
         */
        
        while (step < maxSteps)
        {
            var count = 0;
            
            foreach (var position in _sourcePositions)
            {
                count += Move(position, North);
            
                count += Move(position, South);
            
                count += Move(position, East);
            
                count += Move(position, West);
            }

            if (step > 1)
            {
                _counts[step] = count + _counts[step - 2];
            }
            else
            {
                _counts[step] = count;
            }

            //Console.WriteLine(_counts[step]);
            //
            //Dump();
            
            (_sourcePositions, _targetPositions) = (_targetPositions, _sourcePositions);
            
            _targetPositions.Clear();

            step++;
        }
    }
    
    private void Dump()
    {
        for (var y = 50; y < Height - 50; y++)
        {
            for (var x = 50; x < Width - 50; x++)
            {
                if (_targetPositions.Any(p => p.X == x && p.Y == y))
                    Console.Write("O");
                else
                    Console.Write(' ');
            }
            
            Console.WriteLine();
        }
        
        Console.WriteLine();

        Console.ReadKey();
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

        return _targetPositions.Add(position) ? 1 : 0;
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