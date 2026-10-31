using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._22;

[UsedImplicitly]
public class Part2 : Base
{
    private static int _winningPlayer1Count;

    private static int _winningPlayer2Count;

    public override string GetAnswer()
    {
        ParseInput();

        var count1 = Player1Cards.Count;

        var count2 = Player2Cards.Count;

        var capacity = count1 + count2;

        var player1 = new int[capacity];

        var player2 = new int[capacity];

        Player1Cards.CopyTo(player1, 0);

        Player2Cards.CopyTo(player2, 0);

        var winner = Play(player1, count1, player2, count2);

        var deck = winner == 1 ? player1 : player2;

        var count = winner == 1
            ? _winningPlayer1Count
            : _winningPlayer2Count;

        var score = 0;

        for (var i = 0; i < count; i++)
        {
            score += deck[i] * (count - i);
        }

        return score.ToString();
    }

    private static int Play(int[] player1, int count1, int[] player2, int count2, bool topLevel = true)
    {
        var previous = new HashSet<string>();

        while (count1 > 0 && count2 > 0)
        {
            var state = CreateState(player1, count1, player2, count2);

            if (! previous.Add(state))
            {
                if (topLevel)
                {
                    _winningPlayer1Count = count1;

                    _winningPlayer2Count = count2;
                }

                return 1;
            }

            var card1 = player1[0];

            var card2 = player2[0];

            Array.Copy(player1, 1, player1, 0, --count1);

            Array.Copy(player2, 1, player2, 0, --count2);

            int winner;

            if (count1 >= card1 && count2 >= card2)
            {
                var sub1 = new int[card1 + card2];

                var sub2 = new int[card1 + card2];

                Array.Copy(player1, sub1, card1);

                Array.Copy(player2, sub2, card2);

                winner = Play(sub1, card1, sub2, card2, false);
            }
            else
            {
                winner = card1 > card2 ? 1 : 2;
            }

            if (winner == 1)
            {
                player1[count1++] = card1;

                player1[count1++] = card2;
            }
            else
            {
                player2[count2++] = card2;

                player2[count2++] = card1;
            }
        }

        if (topLevel)
        {
            _winningPlayer1Count = count1;

            _winningPlayer2Count = count2;
        }

        return count1 > 0 ? 1 : 2;
    }

    private static string CreateState(int[] player1, int count1, int[] player2, int count2)
    {
        return string.Create(
            count1 + count2 + 1,
            (player1, count1, player2, count2),
            static (span, state) =>
            {
                for (var i = 0; i < state.count1; i++)
                {
                    span[i] = (char) state.player1[i];
                }

                span[state.count1] = '\0';

                for (var i = 0; i < state.count2; i++)
                {
                    span[state.count1 + 1 + i] = (char) state.player2[i];
                }
            });
    }
}