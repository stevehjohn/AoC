using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._21;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var cost = int.MinValue;

        InitialiseStore();

        InitialisePlayers();

        while (true)
        {
            var newCost = EquipPlayer();

            if (newCost == int.MaxValue)
            {
                break;
            }

            if (ExecuteFight() == 1)
            {
                cost = Math.Max(cost, newCost);
            }

            InitialisePlayers();
        }

        return cost.ToString();
    }
}