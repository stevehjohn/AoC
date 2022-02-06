using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2017._09;

public abstract class Base : Solution
{
    public override string Description => "Stream garbage collection";

    protected static (int Groups, int Garbage) CountGroupsAndGarbage(string stream)
    {
        var count = 0;

        var garbageCount = 0;

        var depth = 0;

        var isGarbage = false;

        for (var i = 0; i < stream.Length; i++)
        {
            var c = stream[i];

            if (c == '!')
            {
                i++;

                continue;
            }

            if (isGarbage && c != '>')
            {
                garbageCount++;
            }

            if (c == '<')
            {
                isGarbage = true;

                continue;
            }

            if (c == '>')
            {
                isGarbage = false;

                continue;
            }

            if (c == '{' && ! isGarbage)
            {
                depth++;

                count += depth;

                continue;
            }

            if (c == '}' && ! isGarbage)
            {
                depth--;
            }
        }

        return (count, garbageCount);
    }
}