using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._22;

[UsedImplicitly]
public class Part2 : Base
{
    private readonly long[] _candidates = new long[4];
    
    public override string GetAnswer()
    {
        var sequences = new List<List<(int Price, int Delta)>>();
        
        foreach (var line in Input)
        {
            sequences.Add(GetSequence(long.Parse(line)));
        }

        var maximumBananas = new List<int>();
        
        for (var i = 9; i > 0; i--)
        {
            var bananas = 0;
            
            GetCandidates(sequences[0], i);
            
            foreach (var sequence in sequences[1..])
            {
                if (FindCandidates(sequence))
                {
                    bananas += i;
                }
            }
            
            maximumBananas.Add(bananas);
        }

        return maximumBananas.Max().ToString();
    }

    private bool FindCandidates(List<(int Price, int Delta)> sequence)
    {
        for (var i = 0; i < sequence.Count - 4; i++)
        {
            var found = true;
            
            for (var j = 0; j < 4; j++)
            {
                if (sequence[i + j].Delta != _candidates[j])
                {
                    found = false;
                    
                    break;
                }
            }

            if (found)
            {
                return true;
            }
        }

        return false;
    }

    private void GetCandidates(List<(int Price, int Delta)> sequence, int maximum)
    {
        var maximumIndex = -1;

        var maximumCount = 0;

        do
        {
            for (var i = 0; i < sequence.Count; i++)
            {
                var item = sequence[i];

                if (item.Price == maximum)
                {
                    maximumCount++;

                    maximumIndex = i;
                }
            }
        } while (maximumCount < 2);

        for (var i = -3; i <= 0; i++)
        {
            _candidates[i + 3] = sequence[maximumIndex + i].Delta;
        }
    }

    private static List<(int Price, int Delta)> GetSequence(long seed)
    {
        var previous = (int) seed % 10;
        
        var sequence = new List<(int Price, int Delta)>();

        for (var i = 0; i < 1_999; i++)
        {
            seed = SimulateRound(seed);

            var price = (int) seed % 10;
            
            sequence.Add((price, price - previous));

            previous = price;
        }

        return sequence;
    }
}