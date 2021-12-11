using AoC.Solutions.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._10;

[UsedImplicitly]
public class Part2 : Base
{
    private const int ElfBet = 200;

    public override string GetAnswer()
    {
        var station = new Point(11, 13);

        Asteroids = Asteroids.Where(a => a.X != station.X || a.Y != station.Y).ToList();

        var asteroidsWithAngle = Asteroids.Select(a => (Angle: Math.Atan2(a.X - station.X, a.Y - station.Y), Point: a)).ToList();

        var angles = asteroidsWithAngle.Select(a => a.Angle).Distinct().OrderByDescending(a => a).ToList();

        var count = 0;

        var lastRemoved = 0;

        while (count < ElfBet)
        {
            foreach (var angle in angles)
            {
                // ReSharper disable once CompareOfFloatsByEqualityOperator - Angles will match exactly
                var asteroidsInLine = asteroidsWithAngle.Where(a => a.Angle == angle).ToList();

                if (! asteroidsInLine.Any())
                {
                    continue;
                }

                var closest = asteroidsInLine.OrderBy(a => Math.Sqrt(Math.Pow(Math.Abs(a.Point.X - station.X), 2) + Math.Pow(Math.Abs(a.Point.Y - station.Y), 2))).First();

                asteroidsWithAngle.Remove(closest);

                lastRemoved = closest.Point.X * 100 + closest.Point.Y;

                count++;

                if (count >= ElfBet)
                {
                    break;
                }
            }
        }

        return lastRemoved.ToString();
    }
}