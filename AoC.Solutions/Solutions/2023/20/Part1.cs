using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._20;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        SendPulse();
        
        return "Unknown";
    }

    private void SendPulse()
    {
    }
}