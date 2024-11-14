using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._19;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        while (Scanners.Any(s => s.Value.Position == null))
        {
            for (var s1 = 0; s1 < Scanners.Count; s1++)
            {
                var originScanner = Scanners[s1];

                if (originScanner.Position == null)
                {
                    continue;
                }

                for (var s2 = 0; s2 < Scanners.Count; s2++)
                {
                    if (s1 == s2 || Scanners[s2].Position != null)
                    {
                        continue;
                    }

                    Scanners[s2].LocateRelativeTo(originScanner);
                }
            }
        }

        var highestDistance = 0;

        for (var s1 = 0; s1 < Scanners.Count; s1++)
        {
            var originScanner = Scanners[s1];

            for (var s2 = 0; s2 < Scanners.Count; s2++)
            {
                if (s1 == s2)
                {
                    continue;
                }

                var distance = originScanner.GetManhattanDistanceFrom(Scanners[s2]);

                if (distance > highestDistance)
                {
                    highestDistance = distance;
                }
            }
        }

        return highestDistance.ToString();
    }
}