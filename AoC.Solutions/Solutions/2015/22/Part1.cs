using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._22;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var cost = int.MaxValue;

        InitialisePlayers();

        InitialiseSpells();

        // TODO: Don't like this approach. 
        for (var i = 0; i < 1_000_000_000; i++)
        {
            var result = ExecuteFight();

            if (result.Winner == 0)
            {
                cost = Math.Min(cost, result.ManaCost);
            }

            InitialisePlayers();
        }

        return cost.ToString();
    }
}