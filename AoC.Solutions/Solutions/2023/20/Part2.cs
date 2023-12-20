using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._20;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var presses = 0;

        while (true)
        {
            presses++;

            var result = SendPulses(true);

            if (result == (0, 0))
            {
                break;
            }
        }
        
        return presses.ToString();
    }
}