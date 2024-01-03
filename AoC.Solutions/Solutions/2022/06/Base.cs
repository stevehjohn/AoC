using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._06;

public abstract class Base : Solution
{
    public override string Description => "Tuning trouble";

    protected int FindMarker(int length)
    {
        var unique = new HashSet<char>();

        int i;

        var data = Input[0];

        for (i = 0; i < data.Length - length + 1; i++)
        {
            for (var x = 0; x < length; x++)
            {
                unique.Add(data[i + x]);
            }

            if (unique.Count == length)
            {
                break;
            }

            unique.Clear();
        }

        return i + length;
    }
}