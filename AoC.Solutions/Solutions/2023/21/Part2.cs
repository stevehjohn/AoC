using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._21;

[UsedImplicitly]
public class Part2 : Base
{
    private const long TargetSteps = 26_501_365;

    private long[] _counts;

    private readonly HashSet<(int X, int Y, int Ux, int Uy, (int X, int Y) D)> _sourcePositions = new();

    private readonly HashSet<(int X, int Y, int Ux, int Uy, (int X, int Y) D)> _targetPositions = new();

    public override string GetAnswer()
    {
        var start = ParseInput();

        Walk(start, Width * 2 + Width / 2 + 1);

        var result = ExtrapolateAnswer();
        
        return result.ToString();
    }

    private void Walk((int X, int Y) start, int maxSteps)
    {
        var oddPositions = new HashSet<(int X, int Y, int Ux, int Uy)>
        {
            (start.X, start.Y, 0, 0)
        };

        var evenPositions = new HashSet<(int X, int Y, int Ux, int Uy)>();
        
        _counts = new long[maxSteps];

        var step = 1;
        
        /*
         * TODO: Just move out from the center tracking new points on the frontier.
         */
        
        while (step < maxSteps)
        {
            var sourcePositions = (step & 1) == 0 ? evenPositions : oddPositions;

            var targetPositions = (step & 1) == 0 ? oddPositions : evenPositions;

            targetPositions.Clear();
            
            foreach (var position in sourcePositions)
            {
                Move(targetPositions, position, -1, 0);
            
                Move(targetPositions, position, 1, 0);
            
                Move(targetPositions, position, 0, -1);
            
                Move(targetPositions, position, 0, 1);
            }

            _counts[step] = targetPositions.Count;

            step++;
        }
    }

    private void Move(HashSet<(int X, int Y, int Ux, int Uy)> positions, (int X, int Y, int Ux, int Uy) position, int dX, int dY)
    {
        position = (position.X + dX, position.Y + dY, position.Ux, position.Uy);

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

        positions.Add(position);
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