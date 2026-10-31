using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2018._09;

public abstract class Base : Solution
{
    public override string Description => "Marble madness";

    protected static long Play(int players, int lastMarbleValue)
    {
        var next = new int[lastMarbleValue + 1];
        
        var previous = new int[lastMarbleValue + 1];

        var scores = new long[players];

        var current = 0;
        
        var player = 0;

        next[0] = 0;
        
        previous[0] = 0;

        for (var marble = 1; marble <= lastMarbleValue; marble++)
        {
            if (marble % 23 == 0)
            {
                var removed = current;

                for (var i = 0; i < 7; i++)
                {
                    removed = previous[removed];
                }

                scores[player] += marble + removed;

                var before = previous[removed];
                
                var after = next[removed];

                next[before] = after;
                
                previous[after] = before;

                current = after;
            }
            else
            {
                var left = next[current];
                
                var right = next[left];

                next[left] = marble;
                
                previous[marble] = left;

                next[marble] = right;
                
                previous[right] = marble;

                current = marble;
            }

            player++;

            if (player == players)
            {
                player = 0;
            }
        }

        return scores.Max();
    }

    protected (int Players, int LastMarbleValue) ParseInput()
    {
        var split = Input[0].Split(' ', StringSplitOptions.TrimEntries);

        return (int.Parse(split[0]), int.Parse(split[6]));
    }
}