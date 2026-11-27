using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2018._05;

public abstract class Base : Solution
{
    public override string Description => "Polymer reactions";

    protected static int ReactPolymer(string polymerString, char exclude = '\0')
    {
        var stack = new char[polymerString.Length];

        var count = 0;

        foreach (var c in polymerString)
        {
            if ((c | 32) == exclude)
            {
                continue;
            }

            if (count > 0 && (stack[count - 1] ^ c) == 32)
            {
                count--;

                continue;
            }

            stack[count++] = c;
        }

        return count;
    }
}