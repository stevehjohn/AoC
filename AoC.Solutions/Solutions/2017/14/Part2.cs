using AoC.Solutions.Solutions._2017.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._14;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var grid = new int [128, 128];

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
                        grid[x, y] = -1;
                    }

                    bit >>= 1;

                    x++;
                }
            }
        }

        return "TESTING";
    }
}