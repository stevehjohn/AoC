using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._21;

[UsedImplicitly]
public class Part2 : Base
{
    private const long TargetSteps = 26_501_365;
    
    public override string GetAnswer()
    {
        var start = ParseInput();

        var result = Walk(start, Width * 2 + Width / 2 + 1);
        
        return result.ToString();
    }

    private long Walk((int X, int Y) start, int maxSteps)
    {
        var oddPositions = new HashSet<(int X, int Y, int Ux, int Uy)>
        {
            (start.X, start.Y, 0, 0)
        };

        var evenPositions = new HashSet<(int X, int Y, int Ux, int Uy)>();
        
        var counts = new long[maxSteps];

        var step = 1;
        
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

            counts[step] = targetPositions.Count;

            step++;
        }

        var halfWidth = Width / 2;
        
        var delta1 = counts[halfWidth + Width] - counts[halfWidth];

        var delta2 = counts[halfWidth + Width * 2] - counts[halfWidth + Width];

        var quotient = TargetSteps / Width;
        
        var result = counts[halfWidth] + delta1 * quotient + quotient * (quotient - 1) / 2 * (delta2 - delta1);
        
        return result;
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
}