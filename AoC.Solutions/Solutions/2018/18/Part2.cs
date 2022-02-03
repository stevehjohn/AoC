using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._18;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        // TODO: There will be a repeating pattern here.
        for (var i = 0; i < 1_000_000_000 ; i++)
        {
            RunCycle();

            if (i % 1_000_000 == 0)
            {
                Console.WriteLine(i);
            }
        }

        return GetResourceValue().ToString();
    }
}