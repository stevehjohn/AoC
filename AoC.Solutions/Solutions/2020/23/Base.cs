using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2020._23;

public abstract class Base : Solution
{
    public override string Description => "Crab cups";

    protected byte[] Cups;

    private byte _cup;

    protected void PerformMove()
    {
        var targetCup = _cup;

        int i;

        var sequenceToShift = 0;

        var removed = new byte[3];

        for (i = 0; i < 4; i++)
        {
            targetCup = Cups[targetCup];

            if (i < 3)
            {
                removed[i] = targetCup;
            }

            if (i == 2)
            {
                sequenceToShift = targetCup;
            }
        }

        targetCup = (byte) (_cup - 1);

        if (targetCup == 0)
        {
            targetCup = 9;
        }

        while (targetCup == removed[0] || targetCup == removed[1] || targetCup == removed[2])
        {
            targetCup--;

            if (targetCup < 1)
            {
                targetCup = 9;
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

        Cups = new byte[data.Length + 1];

        for (var i = 0; i < data.Length - 1; i++)
        {
            Cups[(byte) (data[i] - '0')] = (byte) (data[i + 1] - '0');
        }

        Cups[(byte) (data[^1] - '0')] = (byte) (data[0] - '0');

        _cup = (byte) (Input[0][0] - '0');
    }
}