using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._23;

public abstract class Base : Solution
{
    public override string Description => "LAN party";

    protected const int Size = 26 * 26;

    protected readonly bool[] Adjacent = new bool[Size * Size];

    protected readonly int[][] Neighbours = new int[Size][];

    protected void ParseInput()
    {
        var lists = new List<int>[Size];

        foreach (var line in Input)
        {
            var a = (line[0] - 'a') * 26 + line[1] - 'a';

            var b = (line[3] - 'a') * 26 + line[4] - 'a';

            if (a == b || Adjacent[a * Size + b])
            {
                continue;
            }

            Adjacent[a * Size + b] = true;

            Adjacent[b * Size + a] = true;

            (lists[a] ??= []).Add(b);

            (lists[b] ??= []).Add(a);
        }

        for (var i = 0; i < Size; i++)
        {
            Neighbours[i] = lists[i]?.Order().ToArray() ?? [];
        }
    }
}