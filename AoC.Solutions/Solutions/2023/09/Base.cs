using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._09;

public abstract class Base : Solution
{
    public override string Description => "Mirage maintenance";
    
    protected readonly List<long[]> Sequences = new();

    protected void ParseInput()
    {
        foreach (var line in Input)
        {
            var parts = line.Split(' ', StringSplitOptions.TrimEntries).Select(long.Parse).ToArray();
            
            Sequences.Add(parts);
        }
    }

    protected static long GetHistory(List<long[]> sequences, Func<long[], long, long> selector)
    {
        var last = 0L;
        
        for (var i = sequences.Count - 2; i >= 0; i--)
        {
            last = selector(sequences[i], last);
        }
        
        return last;
    }

    protected static List<long[]> Extrapolate(long[] sequence)
    {
        var sequences = new List<long[]> { sequence };

        while (true)
        {
            var newSequence = new long[sequence.Length - 1];

            var zero = true; 
            
            for (var i = 0; i < sequence.Length - 1; i++)
            {
                var delta = sequence[i + 1] - sequence[i];

                if (delta != 0)
                {
                    zero = false;
                }
                
                newSequence[i] = delta;
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