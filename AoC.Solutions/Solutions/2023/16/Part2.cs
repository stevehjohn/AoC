using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._16;

[UsedImplicitly]
public class Part2 : Base
{
    public Part2()
    {
    }

    public Part2(IVisualiser<PuzzleState> visualiser) : base(visualiser)
    {
    }
    
    public override string GetAnswer()
    {
        ParseInput();

        var max = 0;

        int energised;
        
        for (var x = 0; x < Width; x++)
        {
            SimulateBeams(x, -1, 'S');
        
            energised = CountEnergised();
        
            if (energised > max)
            {
                max = energised;
            }
        }
        
        for (var y = 0; y < Height; y++)
        {
            SimulateBeams(Width, y, 'W');
        
            energised = CountEnergised();
        
            if (energised > max)
            {
                max = energised;
            }
        }
        
        for (var x = Width - 1; x >= 0; x--)
        {
            SimulateBeams(x, Height, 'N');
        
            energised = CountEnergised();
        
            if (energised > max)
            {
                max = energised;
            }
        }
        
        for (var y = Height - 1; y >= 0; y--)
        {
            SimulateBeams(-1, y, 'E');
        
            energised = CountEnergised();
        
            if (energised > max)
            {
                max = energised;
            }
        }
        
        return max.ToString();
    }
}