using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._14;

[UsedImplicitly]
public class Part2 : Base
{
    private readonly Dictionary<int, (int Cycle, int Load)> _seen = new();

    private int _cycle;
    
    private readonly IVisualiser<PuzzleState> _visualiser;
    
    public Part2()
    {
    }

    public Part2(IVisualiser<PuzzleState> visualiser)
    {
        _visualiser = visualiser;
    }

    public override string GetAnswer()
    {
        ParseInput();

        Visualise();
        
        int seenCycle;
        
        while (true)
        {
            var result = PerformCycle();
            
            _cycle++;

            seenCycle = CheckHashState(result.Hash, result.Load);
            
            if (seenCycle > 0)
            {
                break;
            }
        }

        var remainingCycles = (1_000_000_000 - seenCycle) % (_cycle - seenCycle);

        var hashCycle = seenCycle;
        
        for (var i = 0; i < remainingCycles; i++)
        {
            hashCycle++;

            if (hashCycle > _seen.Count)
            {
                hashCycle = seenCycle;
            }
        }
        
        Visualise(seenCycle, hashCycle);

        return _seen.Single(s => s.Value.Cycle == hashCycle).Value.Load.ToString();
    }

    private void Visualise(int patternStart = 0, int patternEnd = 0)
    {
        if (_visualiser != null)
        {
            _visualiser.PuzzleStateChanged(new PuzzleState { Map = Rocks, PatternStart = patternStart, PatternEnd = patternEnd });
        }
    }

    private int CheckHashState(int hash, int load)
    {
        if (! _seen.TryAdd(hash, (_cycle, load)))
        {
            return _seen[hash].Cycle;
        }

        return 0;
    }

    private (int Hash, int Load) PerformCycle()
    {
        MoveRocks(0, -1);
        
        Visualise();
        
        MoveRocks(-1, 0);
        
        Visualise();
        
        MoveRocks(0, 1);
        
        var result = MoveRocks(1, 0);

        Visualise();
        
        return result;
    }
}