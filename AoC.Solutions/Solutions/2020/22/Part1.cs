﻿using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._22;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        Play();

        var score = CalculateScore();

        return score.ToString();
    }

    private int CalculateScore()
    {
        var winnerDeck = (Player1Cards.Count == 0 ? Player2Cards : Player1Cards).Reverse().ToArray();

        var score = 0;

        for (var i = 1; i <= winnerDeck.Length; i++)
        {
            score += i * winnerDeck[i - 1];
        }

        return score;
    }

    private void Play()
    {
        while (Player1Cards.Count > 0 && Player2Cards.Count > 0)
        {
            var player1Draw = Player1Cards.Dequeue();
            
            var player2Draw = Player2Cards.Dequeue();

            if (player1Draw > player2Draw)
            {
                Player1Cards.Enqueue(player1Draw);

                Player1Cards.Enqueue(player2Draw);
            }
            else
            {
                Player2Cards.Enqueue(player2Draw);

                Player2Cards.Enqueue(player1Draw);
            }
        }
    }
}