using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._07;

public abstract class Base : Solution
{
    public override string Description => "Camel cards";

    private List<(string Hand, int Bid)> _hands;

    protected long GetResult(bool jokersWild = false)
    {
        _hands = new();
        
        ParseInput();
        
        var ordered = OrderHands(jokersWild);

        var result = 0L;
        
        for (var i = 0; i < ordered.Count; i++)
        {
            result += ordered[i].Bid * (i + 1);
        }

        return result;
    }

    private List<(int Strength, string Hand, int Bid)> OrderHands(bool jokersWild)
    {
        var ordered = _hands.Select(h => (Strength: GetTypeStrength(jokersWild ? OptimiseHand(h.Hand) : h.Hand), h.Hand, h.Bid)).ToList();

        ordered = ordered.OrderBy(h => h.Strength).ThenBy(h => ParseHandForOrdering(h.Hand, jokersWild)).ToList();
        
        return ordered;
    }
    
    private static string ParseHandForOrdering(string hand, bool jokersWild)
    {
        var newHand = new char[5];
        
        if (jokersWild)
        {
            for (var i = 0; i < 5; i++)
            {
                newHand[i] = hand[i] switch
                {
                    'A' => 'E',
                    'K' => 'D',
                    'Q' => 'C',
                    'J' => '1',
                    'T' => 'A',
                    _ => hand[i]
                };
            }
        }
        else
        {
            for (var i = 0; i < 5; i++)
            {
                newHand[i] = hand[i] switch
                {
                    'A' => 'E',
                    'K' => 'D',
                    'Q' => 'C',
                    'J' => 'B',
                    'T' => 'A',
                    _ => hand[i]
                };
            }
        }

        return new string(newHand);
    }

    private static int GetTypeStrength(string hand)
    {
        var distinct = new Dictionary<char, int>();
        
        for (var i = 0; i < 5; i++)
        {
            if (!distinct.TryAdd(hand[i], 1))
            {
                distinct[hand[i]]++;
            }
        }

        var strength = distinct.Count switch
        {
            1 => 7,
            2 => distinct.Any(c => c.Value == 1) ? 6 : 5,
            3 => distinct.Any(c => c.Value == 3) ? 4 : 3,
            4 => 2,
            5 => 1,
            _ => 0
        };

        return strength;
    }

    private void ParseInput()
    {
        foreach (var line in Input)
        {
            var parts = line.Split(' ', StringSplitOptions.TrimEntries);

            var hand = parts[0];

            _hands.Add((hand, int.Parse(parts[1])));
        }
    }

    private static string OptimiseHand(string hand)
    {
        if (!hand.Contains('J'))
        {
            return hand;
        }

        if (hand == "JJJJJ")
        {
            return "AAAAA";
        }

        var distinct = new int[14];
        
        for (var i = 0; i < 5; i++)
        {
            var item = hand[i];

            if (item == 'J')
            {
                continue;
            }

            item = item switch
            {
                'A' => '=',
                'K' => '<',
                'Q' => ';',
                'T' => ':',
                _ => item
            };

            distinct[item - '0']++;
        }

        var max = 0;
        
        for (var i = 0; i < 14; i++)
        {
            if (distinct[i] > max)
            {
                max = distinct[i];
            }
        }

        var best =' ';
        
        for (var i = 13; i >= 0; i--)
        {
            if (distinct[i] == max)
            {
                best = (char)('0' + (char)i);
            }
        }

        best = best switch
        {
            '=' => 'A',
            '<' => 'K',
            ';' => 'Q',
            ':' => 'T',
            _ => best
        };

        var newHand = new char[5];

        for (var i = 0; i < 5; i++)
        {
            newHand[i] = hand[i] switch
            {
                'J' => best,
                _ => hand[i]
            };
        }

        return new string(newHand);
    }
}