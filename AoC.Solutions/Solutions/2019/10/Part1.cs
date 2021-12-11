#if DEBUG
using System.Diagnostics;
#endif
using AoC.Solutions.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._10;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var maxDetectable = 0;

#if DEBUG
        Point bestScanner = null;
#endif

        foreach (var scanner in Asteroids)
        {
            var detectable = 0;

            foreach (var target in Asteroids)
            {
                if (scanner == target)
                {
                    continue;
                }

                var isBlocked = false;

                foreach (var blocker in Asteroids.Where(b => b.X >= Math.Min(scanner.X, target.X) && b.X <= Math.Max(scanner.X, target.X)
                                                             && b.Y >= Math.Min(scanner.Y, target.Y) && b.Y <= Math.Max(scanner.Y, target.Y)))
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

#if DEBUG
                bestScanner = scanner;
#endif
            }
        }

#if DEBUG
        if (Debugger.IsAttached)
        {
            Debug.WriteLine($"{bestScanner}");
        }
#endif

        return maxDetectable.ToString();
    }
}