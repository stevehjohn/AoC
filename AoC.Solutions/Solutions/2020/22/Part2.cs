using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._22;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        Play(Player1Cards, Player2Cards);

        var score = CalculateScore();

        return score.ToString();
    }

    private static int Play(Queue<int> player1Cards, Queue<int> player2Cards)
    {
        var winner = 0;

        var previousStates = new HashSet<int>();

        while (player1Cards.Count > 0 && player2Cards.Count > 0)
        {
            var stateHash = HashStates(player1Cards, player2Cards);

            if (previousStates.Contains(stateHash))
            {
                return 1;
            }

            previousStates.Add(HashStates(player1Cards, player2Cards));

            var player1Draw = player1Cards.Dequeue();
            
            var player2Draw = player2Cards.Dequeue();

            winner = player1Draw > player2Draw ? 1 : 2;

            if (player1Cards.Count >= player1Draw && player2Cards.Count >= player2Draw)
            {
                winner = Play(new Queue<int>(player1Cards.Take(player1Draw)), new Queue<int>(player2Cards.Take(player2Draw)));
            }

            if (winner == 1)
            {
                player1Cards.Enqueue(player1Draw);

                player1Cards.Enqueue(player2Draw);
            }
            else
            {
                player2Cards.Enqueue(player2Draw);

                player2Cards.Enqueue(player1Draw);
            }
        }

        return winner;
    }

    private static int HashStates(Queue<int> player1Cards, Queue<int> player2Cards)
    {
        var p1Array = player1Cards.ToArray();

        var p2Array = player2Cards.ToArray();

        var p1Hash = 0;

        var p2Hash = 0;

        for (var i = 0; i < p1Array.Length; i++)
        {
            p1Hash = HashCode.Combine(p1Hash, p1Array[i]);
        }

        for (var i = 0; i < p2Array.Length; i++)
        {
            p2Hash = HashCode.Combine(p1Hash, p2Array[i]);
        }

        return HashCode.Combine(p1Hash, p2Hash);
    }
}