using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._16;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        SimulateBeams(-1, 0);
        
        return CountEnergised().ToString();
    }
}