using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._13;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var firewall = ParseInput();

        delays: for (var delay = 0; ; delay++)
        {
            foreach (var (layer, value) in firewall)
            {
                if ((delay + layer) % value.Period == 0)
                {
                    continue delays;
                }
            }

            return delay.ToString();
        }
    }
}