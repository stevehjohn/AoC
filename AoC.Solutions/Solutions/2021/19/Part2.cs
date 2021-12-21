using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._19;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        for (var s1 = 0; s1 < Scanners.Count; s1++)
        {
            var originScanner = Scanners[s1];

            for (var s2 = s1 + 1; s2 < Scanners.Count; s2++)
            {
                originScanner.LocateRelativeTo(Scanners[s2]);
            }
        }

        return "TESTING";
    }
}