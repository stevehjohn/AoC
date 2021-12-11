using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._10;

[UsedImplicitly]
public class Part1 : Solution
{
    public override string GetAnswer()
    {
        var asteroids = new List<Point>();

        for (var y = 0; y < Input.Length; y++)
        {
            for (var x = 0; x < Input[y].Length; x++)
            {
                if (Input[y][x] == '#')
                {
                    asteroids.Add(new Point(x, y));
                }
            }
        }

        var maxDetectable = 0;

        foreach (var scanner in asteroids)
        {
            var detectable = 0;

            foreach (var target in asteroids)
            {
                if (scanner == target)
                {
                    continue;
                }

                var isBlocked = false;

                foreach (var blocker in asteroids)
                {
                    if (blocker == scanner || blocker == target)
                    {
                        continue;
                    }

                    isBlocked = IsBlocking(scanner, target, blocker);
                    
                    if (isBlocked)
                    {
                        break;
                    }
                }

                if (! isBlocked)
                {
                    detectable++;
                }
            }

            if (detectable > maxDetectable)
            {
                maxDetectable = detectable;
            }
        }

        return maxDetectable.ToString();
    }

    private static bool IsBlocking(Point scanner, Point target, Point blocker)
    {
        return (blocker.X - scanner.X) * (target.Y - scanner.Y) - (blocker.Y - scanner.Y) * (target.X - scanner.X) == 0;
    }
}