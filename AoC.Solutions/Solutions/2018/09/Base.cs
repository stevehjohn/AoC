using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2018._09;

public abstract class Base : Solution
{
    public override string Description => "Marble madness";

    protected static long Play(int players, int lastMarbleValue)
    {
        var circle = new LinkedList<int>();

        var player = 0;

        var playerScores = new Dictionary<int, long>();

        var nextMarble = 1;

        var currentMarble = circle.AddFirst(0);

        while (nextMarble <= lastMarbleValue)
        {
            if (nextMarble % 23 == 0)
            {
                if (! playerScores.TryAdd(player, nextMarble))
                {
                    playerScores[player] += nextMarble;
                }

                var toRemove = currentMarble.PreviousCircular().PreviousCircular().PreviousCircular().PreviousCircular().PreviousCircular().PreviousCircular().PreviousCircular();

                playerScores[player] += toRemove.Value;

                currentMarble = toRemove.NextCircular();

                circle.Remove(toRemove);
            }
            else
            {
                currentMarble = currentMarble.NextCircular();

                currentMarble = circle.AddAfter(currentMarble, nextMarble);
            }

            nextMarble++;

            player = (player + 1) % players;
        }

        return playerScores.Max(s => s.Value);
    }

    protected (int Players, int LastMarbleValue) ParseInput()
    {
        var split = Input[0].Split(' ', StringSplitOptions.TrimEntries);

        return (int.Parse(split[0]), int.Parse(split[6]));
    }
}