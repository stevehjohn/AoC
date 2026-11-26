using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._06;

public abstract class Base : Solution
{
    public override string Description => "Tuning trouble";

    protected int FindMarker(int length)
    {
        var unique = new HashSet<char>();

        var data = Input[0];

        int i;

        windows: for (i = 0; i < data.Length - length + 1; i++)
        {
            unique.Clear();

            for (var x = 0; x < length; x++)
            {
                if (! unique.Add(data[i + x]))
                {
                    continue windows;
                }
            }

            break;
        }

        return i + length;
    }
}