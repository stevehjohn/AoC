using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._21;

[UsedImplicitly]
public class Part1 : Base
{
    public override string Description => "Quantum dice";

    public override string GetAnswer()
    {
        ParseInput();

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
                Player1Position += roll;

                while (Player1Position > 10)
                {
                    Player1Position -= 10;
                }

                player1Score += Player1Position;
            }
            else
            {
                Player2Position += roll;

                while (Player2Position > 10)
                {
                    Player2Position -= 10;
                }

                player2Score += Player2Position;
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