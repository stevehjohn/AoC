using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._19;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer() => SolveScanners().MaxScannerDistance.ToString();
}
