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

        while (true)
        {
            var result = ExecuteFight();

            if (result.Winner == 0)
            {
                cost = Math.Min(cost, result.ManaCost);
            }

            InitialisePlayers();
        }

        return "TESTING";
    }
}