using AoC.Solutions.Solutions._2017.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._14;

[UsedImplicitly]
public class Part2 : Base
{
    private readonly int[,] _disk = new int[128, 128];

    public override string GetAnswer()
    {
        BuildDiskImage();

        var result = CountRegions();

        return result.ToString();
    }

    private int CountRegions()
    {
        var distinct = new HashSet<int>();

        for (var y = 0; y < 128; y++)
        {
            for (var x = 0; x < 128; x++)
            {
                if (_disk[x, y] != 0)
                {
                    distinct.Add(_disk[x, y]);
                }
            }
        }

        return distinct.Count;
    }

    private void BuildDiskImage()
    {
        for (var y = 0; y < 128; y++)
        {
            //var rowHash = KnotHash.MakeHash($"{Input[0]}-{y}").ToList();
            var rowHash = KnotHash.MakeHash($"flqrgnkx-{y}").ToList();

            var x = 0;

            foreach (var c in rowHash)
            {
                var b = "0123456789abcdef".IndexOf(c);

                var bit = 8;

                for (var i = 0; i < 4; i++)
                {
                    if ((b & bit) > 0)
                    {
                        _disk[x, y] = -1;
                    }

                    bit >>= 1;

                    x++;
                }
            }
        }
    }
}