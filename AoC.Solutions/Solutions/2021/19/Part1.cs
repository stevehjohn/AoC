using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._19;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        for (var s1 = 0; s1 < Scanners.Count; s1++)
        {
            var originScanner = Scanners[s1];

            for (var s2 = s1 + 1; s2 < Scanners.Count; s2++)
            {
                originScanner.RemoveMatchingBeacons(Scanners[s2]);
            }
        }

        var beaconCount = 0;

        for (var s = 0; s < Scanners.Count; s++)
        {
            beaconCount += Scanners[s].BeaconCount;
        }

        return beaconCount.ToString();
    }
}