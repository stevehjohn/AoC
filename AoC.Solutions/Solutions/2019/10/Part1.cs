using AoC.Solutions.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._10;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var maxDetectable = 0;

        Point bestScanner = null;

        foreach (var scanner in Asteroids)
        {
            var detectable = Asteroids.Where(a => ! Equals(a, scanner)).Select(a => Math.Atan2(a.X - scanner.X, a.Y - scanner.Y)).Distinct().Count();

            if (detectable > maxDetectable)
            {
                maxDetectable = detectable;

                bestScanner = scanner;
            }
        }

        // ReSharper disable once PossibleNullReferenceException
        File.WriteAllText("2019.10.1.result", $"{bestScanner.X}, {bestScanner.Y}");

        return maxDetectable.ToString();
    }
}