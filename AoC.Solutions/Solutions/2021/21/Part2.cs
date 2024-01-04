using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._21;

[UsedImplicitly]
public class Part2 : Base
{
    private readonly (int Roll, int Probability)[] _rollProbabilities = [(3, 1), (4, 3), (5, 6), (6, 7), (7, 6), (8, 3), (9, 1)];

    public override string GetAnswer()
    {
        ParseInput();

        var player1Wins = GetPlayerWins(true);

        var player2Wins = GetPlayerWins(false);

        return Math.Max(player1Wins, player2Wins).ToString();
    }

    private long GetPlayerWins(bool evaluatePlayer1)
    {
        var scores = (Player1: 0, Player2: 0);

        var positions = (Player1: Player1Position, Player2: Player2Position);

        var wins = 0L;

        foreach (var probability in _rollProbabilities)
        {
            wins += Play(scores, positions, true, probability.Roll, evaluatePlayer1) * probability.Probability;
        }

        return wins;
    }

    private long Play((int Player1, int Player2) scores, (int Player1, int Player2) positions, bool player1Turn, int roll, bool evaluatePlayer1)
    {
        var wins = 0L;

        if (player1Turn)
        {
            positions.Player1 += roll;

            if (positions.Player1 > 10)
            {
                positions.Player1 -= 10;
            }

            scores.Player1 += positions.Player1;
        }
        else
        {
            positions.Player2 += roll;

            if (positions.Player2 > 10)
            {
                positions.Player2 -= 10;
            }

            scores.Player2 += positions.Player2;
        }

        if (scores.Player1 >= 21 || scores.Player2 >= 21)
        {
            if (evaluatePlayer1)
            {
                return scores.Player1 >= 21 ? 1 : 0;
            }

            return scores.Player2 >= 21 ? 1 : 0;
        }

        player1Turn = ! player1Turn;

        foreach (var probability in _rollProbabilities)
        {
            wins += Play(scores, positions, player1Turn, probability.Roll, evaluatePlayer1) * probability.Probability;
        }

        return wins;
    }
}