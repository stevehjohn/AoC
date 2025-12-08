using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._21;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var seen = new HashSet<int>();
        
        var last = 0;

        var r5 = 0;

        while (true)
        {
            var r3 = r5 | 65536;
            r5 = 7586220;

            while (true)
            {
                var r1 = r3 & 255;
                unchecked
                {
                    r5 = (r5 + r1) & 0xFFFFFF;
                    r5 = (r5 * 65899) & 0xFFFFFF;
                }

                if (r3 < 256)
                {
                    break;
                }

                r3 /= 256;
            }

            // At this point, r5 is exactly the value compared against r0 at `eqrr 5 0 1`.

            if (!seen.Add(r5))
            {
                // First repeat; last is the final unique value that gives the longest run
                return last.ToString();
            }

            last = r5;
        }
    }
}