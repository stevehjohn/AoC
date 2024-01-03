using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._21;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var cost = int.MaxValue;

        InitialiseStore();

        InitialisePlayers();

        while (true)
        {
            var newCost = EquipPlayer();

            if (newCost == int.MaxValue)
            {
                break;
            }

            if (ExecuteFight() == 0)
            {
                cost = Math.Min(cost, newCost);
            }

            InitialisePlayers();
        }

        return cost.ToString();
    }
}