using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2020._23;

public abstract class Base : Solution
{
    public override string Description => "Crab cups";

    protected int[] Cups;
    
    protected int Max;

    private int _cup;

    protected void PerformMove()
    {
        var targetCup = _cup;

        int i;

        var sequenceToShift = 0;

        var removed = new int[3];

        for (i = 0; i < 3; i++)
        {
            targetCup = Cups[targetCup];

            removed[i] = targetCup;

            if (i == 2)
            {
                sequenceToShift = targetCup;
            }
        }

        targetCup = _cup - 1;

        if (targetCup == 0)
        {
            targetCup = Max;
        }

        while (targetCup == removed[0] || targetCup == removed[1] || targetCup == removed[2])
        {
            targetCup--;

            if (targetCup < 1)
            {
                targetCup = Max;
            }
        }

        var nextCup = Cups[_cup];

        Cups[_cup] = Cups[sequenceToShift];

        Cups[sequenceToShift] = Cups[targetCup];

        Cups[targetCup] = nextCup;

        _cup = Cups[_cup];
    }

    protected void ParseInput()
    {
        var data = Input[0];

        Cups = new int[data.Length + 1];

        for (var i = 0; i < data.Length - 1; i++)
        {
            Cups[data[i] - '0'] = data[i + 1] - '0';
        }

        Cups[data[^1] - '0'] = data[0] - '0';

        _cup = Input[0][0] - '0';

        Max = data.Length;
    }
}