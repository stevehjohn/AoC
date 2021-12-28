using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._21;

[UsedImplicitly]
public class Part2 : Base
{
    private readonly (int Roll, int Probability)[] _rollProbabilities = { (3, 1), (4, 3), (5, 6), (6, 7), (7, 6), (8, 3), (9, 1) };

    public override string GetAnswer()
    {
        ParseInput();

        var scores = (Player1: 0, Player2: 0);

        var positions = (Player1: Player1Position, Player2: Player2Position);

        var result = Play(scores, positions, true);

        return Math.Max(result.Item1, result.Item2).ToString();
    }

    private (long, long) Play((int Player1, int Player2) scores, (int Player1, int Player2) positions, bool player1, int universes = 1)
    {
        var result = (0L, 0L);

        foreach (var probability in _rollProbabilities)
        {
            if (player1)
            {
                positions.Player1 += probability.Roll;

                if (positions.Player1 > 10)
                {
                    positions.Player1 -= 10;
                }

                scores.Player1 += positions.Player1;
            }
            else
            {
                positions.Player2 += probability.Roll;

                if (positions.Player2 > 10)
                {
                    positions.Player2 -= 10;
                }

                scores.Player2 += positions.Player2;
            }

            if (scores.Player1 >= 21)
            {
                return (universes, 0);
            }

            if (scores.Player2 >= 21)
            {
                return (0, universes);
            }

            var next = Play(scores, positions, ! player1, universes * probability.Probability);

            result.Item1 += next.Item1;

            result.Item2 += next.Item2;
        }

        return result;
    }
}