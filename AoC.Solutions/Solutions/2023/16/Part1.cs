using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._16;

[UsedImplicitly]
public class Part1 : Base
{
    public Part1()
    {
    }

    public Part1(IVisualiser<PuzzleState> visualiser) : base(visualiser)
    {
    }
    
    public override string GetAnswer()
    {
        ParseInput();

        Visualise();
        
        SimulateBeams(-1, 0, 'E');
        
        return CountEnergised().ToString();
    }
}