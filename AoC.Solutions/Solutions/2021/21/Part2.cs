using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._21;

[UsedImplicitly]
public class Part2 : Base
{
    private static readonly (int Roll, int Frequency)[] RollProbabilities =
    [
        (3, 1), (4, 3), (5, 6), (6, 7), (7, 6), (8, 3), (9, 1)
    ];

    private readonly Dictionary<(int P1, int P2, int S1, int S2, bool Turn), (long W1, long W2)> _cache = new();

    public override string GetAnswer()
    {
        ParseInput();

        _cache.Clear();

        var (p1Wins, p2Wins) = Play(Player1Position, Player2Position, 0, 0, true);

        return Math.Max(p1Wins, p2Wins).ToString();
    }

    private (long W1, long W2) Play(int p1, int p2, int s1, int s2, bool player1Turn)
    {
        if (s1 >= 21)
        {
            return (1, 0);
        }

        if (s2 >= 21)
        {
            return (0, 1);
        }

        var key = (p1, p2, s1, s2, player1Turn);

        if (_cache.TryGetValue(key, out var cached))
        {
            return cached;
        }

        long wins1 = 0;

        long wins2 = 0;

        foreach (var (roll, frequency) in RollProbabilities)
        {
            (long w1, long w2) result;

            if (player1Turn)
            {
                var next = (p1 + roll - 1) % 10 + 1;

                result = Play(next, p2, s1 + next, s2, false);
            }
            else
            {
                var next = (p2 + roll - 1) % 10 + 1;

                result = Play(p1, next, s1, s2 + next, true);
            }

            wins1 += result.w1 * frequency;

            wins2 += result.w2 * frequency;
        }

        var answer = (wins1, wins2);

        _cache[key] = answer;

        return answer;
    }
}