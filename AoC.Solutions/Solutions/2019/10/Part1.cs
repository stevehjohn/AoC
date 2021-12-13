#if DEBUG
using System.Diagnostics;
using AoC.Solutions.Common;
#endif
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
            var detectable = Asteroids.Where(a => a != scanner).Select(a => Math.Atan2(a.X - scanner.X, a.Y - scanner.Y)).Distinct().Count();

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
            // ReSharper disable once PossibleNullReferenceException
            Debug.WriteLine($"{bestScanner.X}, {bestScanner.Y}");
        }
#endif

        return maxDetectable.ToString();
    }
}