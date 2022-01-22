using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._22;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly Queue<int> _player1Cards = new();

    private readonly Queue<int> _player2Cards = new();

    public override string GetAnswer()
    {
        ParseInput();

        Play();

        var score = CalculateScore();

        return score.ToString();
    }

    private int CalculateScore()
    {
        var winnerDeck = (_player1Cards.Count == 0 ? _player2Cards : _player1Cards).Reverse().ToArray();

        var score = 0;

        for (var i = 1; i <= winnerDeck.Length; i++)
        {
            score += i * winnerDeck[i - 1];
        }

        return score;
    }

    private void Play()
    {
        while (_player1Cards.Count > 0 && _player2Cards.Count > 0)
        {
            var player1Draw = _player1Cards.Dequeue();
            
            var player2Draw = _player2Cards.Dequeue();

            if (player1Draw > player2Draw)
            {
                _player1Cards.Enqueue(player1Draw);

                _player1Cards.Enqueue(player2Draw);
            }
            else
            {
                _player2Cards.Enqueue(player2Draw);

                _player2Cards.Enqueue(player1Draw);
            }
        }
    }

    private void ParseInput()
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
                _player1Cards.Enqueue(int.Parse(line));
            }
            else
            {
                _player2Cards.Enqueue(int.Parse(line));
            }
        }
    }
}