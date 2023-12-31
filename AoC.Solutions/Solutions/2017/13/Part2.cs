using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._13;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var firewall = ParseInput();

        var delay = 0;

        while (true)
        {
            var caught = false;

            foreach (var (layer, value) in firewall)
            {
                if ((delay + layer) % value.Period == 0)
                {
                    caught = true;

                    break;
                }
            }

            if (! caught)
            {
                break;
            }

            delay++;
        }

        return delay.ToString();
    }
}