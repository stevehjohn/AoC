using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._07;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly List<(string Hand, int Bid)> _hands = new();
    
    public override string GetAnswer()
    {
        ParseInput();

        var ordered = OrderHands();

        foreach (var hand in ordered)
        {
            Console.WriteLine(hand.Hand);
        }

        var result = 0;
        
        for (var i = 0; i < ordered.Count; i++)
        {
            result += ordered[i].Bid * (i + 1);
        }

        return result.ToString();
    }

    private List<(int Strength, string Hand, int Bid)> OrderHands()
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