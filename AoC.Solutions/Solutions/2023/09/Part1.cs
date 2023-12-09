using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._09;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var total = 0L;

        foreach (var sequence in Sequences)
        {
            total += GetHistory(sequence);
        }
        
        return total.ToString();
    }

    private long GetHistory(List<long> sequence)
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

        for (var i = sequences.Count - 2; i >= 0; i--)
        {
            sequences[i].Add(sequences[i].Last() + sequences[i + 1]. Last());
            //
            // Console.WriteLine(string.Join(", ", sequences[i]));
        }
        
        return sequences[0].Last();
    }
}