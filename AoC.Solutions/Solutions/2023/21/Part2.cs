using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._21;

[UsedImplicitly]
public class Part2 : Base
{
    private const long TargetSteps = 26_501_365;

    private long[] _counts;

    private HashSet<(int X, int Y, int Ux, int Uy, (int X, int Y) Direction)> _sourcePositions = new();

    private HashSet<(int X, int Y, int Ux, int Uy, (int X, int Y) Direction)> _targetPositions = new();

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
        _sourcePositions.Add((start.X, start.Y, 0, 0, North));
        _sourcePositions.Add((start.X, start.Y, 0, 0, South));
        _sourcePositions.Add((start.X, start.Y, 0, 0, East));
        _sourcePositions.Add((start.X, start.Y, 0, 0, West));
        
        _counts = new long[maxSteps];

        var step = 1;
        
        /*
         * TODO: Just move out from the center tracking new points on the frontier.
         */
        
        while (step < maxSteps)
        {
            foreach (var position in _sourcePositions)
            {
                Move(position, North);
            
                Move(position, South);
            
                Move(position, East);
            
                Move(position, West);
            }

            _counts[step] = _targetPositions.DistinctBy(p => new { p.X, p.Y, p.Ux, p.Uy }).Count() + _counts[step - 1];

            Console.WriteLine(_counts[step]);
            
            Dump();
            
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
    
    private void Move((int X, int Y, int Ux, int Uy, (int X, int Y) Direction) position, (int X, int Y) direction)
    {
        if (position.Direction == North && direction == South)
        {
            return;
        }

        if (position.Direction == South && direction == North)
        {
            return;
        }

        if (position.Direction == East && direction == West)
        {
            return;
        }

        if (position.Direction == West && direction == East)
        {
            return;
        }

        position = (position.X + direction.X, position.Y + direction.Y, position.Ux, position.Uy, direction);

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
            return;
        }

        _targetPositions.Add(position);
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