using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2020._22;

public abstract class Base : Solution
{
    public override string Description => "Crab cards";

    protected readonly Queue<int> Player1Cards = new();

    protected readonly Queue<int> Player2Cards = new();

    protected int CalculateScore()
    {
        var winnerDeck = (Player1Cards.Count == 0 ? Player2Cards : Player1Cards).Reverse().ToArray();

        var score = 0;

        for (var i = 1; i <= winnerDeck.Length; i++)
        {
            score += i * winnerDeck[i - 1];
        }

        return score;
    }

    protected void ParseInput()
    {
        var player1 = true;

        foreach (var line in Input.Skip(1))
        {
            if (string.IsNullOrWhiteSpace(line) || line[0] == 'P')
            {
                player1 = false;

                continue;
            }

            if (player1)
            {
                Player1Cards.Enqueue(int.Parse(line));
            }
            else
            {
                Player2Cards.Enqueue(int.Parse(line));
            }
        }
    }
}