using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._13;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var firewall = ParseInput();

        var delay = 0;

        while (GetSeverity(firewall, delay).Caught)
        {
            delay++;

            foreach (var (_, value) in firewall)
            {
                value.ResetScanner();
            }
        }

        return delay.ToString();
    }
}