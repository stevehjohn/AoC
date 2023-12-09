using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._09;

public abstract class Base : Solution
{
    public override string Description => "Mirage maintenance";
    
    protected readonly List<List<long>> Sequences = new();

    protected void ParseInput()
    {
        foreach (var line in Input)
        {
            var parts = line.Split(' ', StringSplitOptions.TrimEntries).Select(long.Parse).ToList();
            
            Sequences.Add(parts);
        }
    }

    protected static List<List<long>> Extrapolate(List<long> sequence)
    {
        var sequences = new List<List<long>> { sequence };

        while (true)
        {
            var newSequence = new List<long>();

            var zero = true; 
            
            for (var i = 0; i < sequence.Count - 1; i++)
            {
                var delta = sequence[i + 1] - sequence[i];

                if (delta != 0)
                {
                    zero = false;
                }
                
                newSequence.Add(delta);
            }
            
            sequences.Add(newSequence);
            
            if (zero)
            {
                break;
            }

            sequence = newSequence;
        }

        return sequences;
    }
}