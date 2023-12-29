using AoC.Solutions.Extensions;
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
        const int bufferSize = 7;

        var positions = new HashSet<(int X, int Y, int Ux, int Uy)>[bufferSize];

        for (var i = 0; i < bufferSize; i++)
        {
            positions[i] = new HashSet<(int X, int Y, int Ux, int Uy)>();
        }

        positions[0].Add((start.X, start.Y, 0, 0));

        var source = 0;

        var target = bufferSize - 1;
        
        var counts = new long[maxSteps];

        var step = 1;
        
        while (step < maxSteps)
        {
            var sourcePositions = positions[source];

            var targetPositions = positions[target];

            var count = 0;
                        
            targetPositions.Clear();

            foreach (var position in sourcePositions)
            {
                count += Move(targetPositions, position, -1, 0);
            
                count += Move(targetPositions, position, 1, 0);
            
                count += Move(targetPositions, position, 0, -1);
            
                count += Move(targetPositions, position, 0, 1);
            }

            counts[step] = targetPositions.Count;

            if (step > 6)
            {
                count = 0;

                var y = new HashSet<(int X, int Y, int Ux, int Uy)>();

                var x = new HashSet<(int X, int Y, int Ux, int Uy)>(sourcePositions);

                var t = source;

                for (var i = 0; i < bufferSize - 2; i++)
                {
                    t = t.DecRotate(bufferSize - 1);
                }

                x.ExceptWith(positions[t]);
                
                foreach (var position in x)
                {
                    count += Move(y, position, -1, 0);

                    count += Move(y, position, 1, 0);

                    count += Move(y, position, 0, -1);

                    count += Move(y, position, 0, 1);
                }

                if (count + counts[step - 4] == counts[step])
                {
                    Console.WriteLine($"\u2713: {counts[step]} == Σ: {count + counts[step - 4]} Δ: {count} ");
                }
                else
                {
                    Console.WriteLine($"\u2713: {counts[step]} != Σ: {count + counts[step - 4]} Δ: {count} Diff: {counts[step] - (count + counts[step - 4])} Step: {step}");
                }
            }
            else
            {
                Console.WriteLine($"\u2713: {counts[step]} ");

            }

            step++;

            source = source.DecRotate(bufferSize - 1);
            
            target = target.DecRotate(bufferSize - 1);
        }

        var halfWidth = Width / 2;
        
        var delta1 = counts[halfWidth + Width] - counts[halfWidth];

        var delta2 = counts[halfWidth + Width * 2] - counts[halfWidth + Width];

        var quotient = TargetSteps / Width;
        
        var result = counts[halfWidth] + delta1 * quotient + quotient * (quotient - 1) / 2 * (delta2 - delta1);
        
        return result;
    }

    private int Move(HashSet<(int X, int Y, int Ux, int Uy)> positions, (int X, int Y, int Ux, int Uy) position, int dX, int dY)
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
            return 0;
        }

        return positions.Add(position) ? 1 : 0;
    }
}