namespace AoC.Solutions.Solutions._2023._20;

public class Module
{
    public Type Type { get; set; }

    public bool State { get; set; }

    public List<string> Targets { get; set; }

    public Dictionary<string, bool> ReceivedPulses { get; set; }
}