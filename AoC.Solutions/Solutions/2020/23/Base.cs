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
        var cups = Cups;
        
        var current = _cup;

        var first = cups[current];
        
        var second = cups[first];
        
        var third = cups[second];

        var destination = current - 1;

        if (destination == 0)
        {
            destination = Max;
        }

        while (destination == first || destination == second || destination == third)
        {
            destination--;

            if (destination == 0)
            {
                destination = Max;
            }
        }

        cups[current] = cups[third];

        cups[third] = cups[destination];

        cups[destination] = first;

        _cup = cups[current];
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

        _cup = data[0] - '0';

        Max = data.Length;
    }
}