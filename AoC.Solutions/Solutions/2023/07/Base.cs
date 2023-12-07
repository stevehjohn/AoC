using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._07;

public abstract class Base : Solution
{
    public override string Description => "Camel cards";

    private readonly List<(string Hand, int Bid)> _hands = new();

    protected int GetAnswerInternal(bool jokersWild = false)
    {
        ParseInput();

        var ordered = OrderHands(jokersWild);

        var result = 0;
        
        for (var i = 0; i < ordered.Count; i++)
        {
            result += ordered[i].Bid * (i + 1);
        }

        return result;
    }

    private List<(int Strength, string Hand, int Bid)> OrderHands(bool jokersWild)
    {
        var ordered = _hands.Select(h => (Strength: GetTypeStrength(h.Hand), h.Hand, h.Bid)).ToList();

        ordered = ordered.OrderBy(h => h.Strength).ThenBy(h => ParseHandForOrdering(h.Hand)).ToList();
        
        return ordered;
    }

    private string ParseHandForOrdering(string hand)
    {
        return hand.Replace('A', 'E').Replace('K', 'D').Replace('Q', 'C').Replace('J', 'B').Replace('T', 'A');
    }

    private int GetTypeStrength(string hand)
    {
        var distinct = new Dictionary<char, int>();
        
        for (var i = 0; i < 5; i++)
        {
            if (distinct.ContainsKey(hand[i]))
            {
                distinct[hand[i]]++;
            }
            else
            {
                distinct.Add(hand[i], 1);
            }
        }
        
        var strength = distinct.Count switch
        {
            1 => 7,
            2 => distinct.Any(c => c.Value == 1) ? 6 : 5,
            3 => distinct.Any(c => c.Value == 3) ? 4 : 3,
            4 => 2,
            5 => 1,
            _ => 0,
        };

        return strength;
    }

    private void ParseInput()
    {
        foreach (var line in Input)
        {
            var parts = line.Split(' ', StringSplitOptions.TrimEntries);
            
            _hands.Add((parts[0], int.Parse(parts[1])));
        }
    }
    
}