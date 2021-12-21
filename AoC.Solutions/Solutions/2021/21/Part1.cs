using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._21;

[UsedImplicitly]
public class Part1 : Solution
{
    public override string Description => "";

    public override string GetAnswer()
    {
        var player1Position = 5;

        var player2Position = 10;

        var player1Score = 0;

        var player2Score = 0;

        var die = 100;

        var player = 1;

        var rolls = 0;

        while (player1Score < 1000 && player2Score < 1000)
        {
            rolls += 3;

            var roll = Roll(ref die) + Roll(ref die) + Roll(ref die);

            if (player == 1)
            {
                player1Position += roll;

                while (player1Position > 10)
                {
                    player1Position -= 10;
                }

                player1Score += player1Position;
            }
            else
            {
                player2Position += roll;

                while (player2Position > 10)
                {
                    player2Position -= 10;
                }

                player2Score += player2Position;
            }

            player++;

            if (player > 2)
            {
                player = 1;
            }
        }

        return (Math.Min(player1Score, player2Score) * rolls).ToString();
    }

    private static int Roll(ref int dieState)
    {
        dieState++;

        if (dieState > 100)
        {
            dieState = 1;
        }

        return dieState;
    }
}