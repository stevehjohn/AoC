using AoC.Solutions.Libraries;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._12;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var x = FindPeriod(Moons[0].Position.X, Moons[1].Position.X, Moons[2].Position.X, Moons[3].Position.X);

        var y = FindPeriod(Moons[0].Position.Y, Moons[1].Position.Y, Moons[2].Position.Y, Moons[3].Position.Y);

        var z = FindPeriod(Moons[0].Position.Z, Moons[1].Position.Z, Moons[2].Position.Z, Moons[3].Position.Z);

        return Maths.LowestCommonMultiple([x, y, z]).ToString();
    }

    private static int FindPeriod(int a, int b, int c, int d)
    {
        var p0 = a;
        var p1 = b;
        var p2 = c;
        var p3 = d;

        var v0 = 0;
        var v1 = 0;
        var v2 = 0;
        var v3 = 0;

        var steps = 0;

        do
        {
            var ab = Math.Sign(p1 - p0);
            var ac = Math.Sign(p2 - p0);
            var ad = Math.Sign(p3 - p0);
            var bc = Math.Sign(p2 - p1);
            var bd = Math.Sign(p3 - p1);
            var cd = Math.Sign(p3 - p2);

            v0 += ab + ac + ad;
            v1 += bc + bd - ab;
            v2 += cd - ac - bc;
            v3 -= ad + bd + cd;

            p0 += v0;
            p1 += v1;
            p2 += v2;
            p3 += v3;

            steps++;
        } while ((v0 | v1 | v2 | v3) != 0 ||
                 p0 != a || p1 != b || p2 != c || p3 != d);

        return steps;
    }
}