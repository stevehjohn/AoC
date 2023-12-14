using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._14;

[UsedImplicitly]
public class Part2 : Base
{
    private readonly Dictionary<int, (int Cycle, int Load)> _seen = new();

    private int _cycle;
    
    public override string GetAnswer()
    {
        ParseInput();

        int seenCycle;
        
        while (true)
        {
            PerformCycle();
            
            break;
            
            _cycle++;

            seenCycle = CheckHashState();
            
            if (seenCycle > 0)
            {
                break;
            }
        }

        return "0";

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

        var result = _seen.Single(s => s.Value.Cycle == hashCycle).Value.Load;

        return result.ToString();
    }

    private int CheckHashState()
    {
        var hash = 0;

        for (var y = 0; y < Rows; y++)
        {
            var rowString = new char[Columns];
            
            for (var x = 0; x < Columns; x++)
            {
                rowString[x] = Rocks[x, y];
            }

            hash = HashCode.Combine(hash, new string(rowString).GetHashCode());
        }

        if (! _seen.TryAdd(hash, (_cycle, GetLoad())))
        {
            return _seen[hash].Cycle;
        }

        return 0;
    }

    private void PerformCycle()
    {
        MoveRocks(0, -1);
        
        MoveRocks(-1, 0);
        
        MoveRocks(0, 1);
        
        MoveRocks(1, 0);
    }
}