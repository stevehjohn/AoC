using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._16;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var dancers = new char[16];

        for (var i = 0; i < 16; i++)
        {
            dancers[i] = (char)('a' + i);
        }

        var previous = new HashSet<string>
                       {
                           new(dancers)
                       };

        var cycleLength = 0;

        for (var i = 0; i < 1_000_000_000; i++)
        {
            RunDance(ref dancers, Input[0]);

            var state = new string(dancers);

            if (! previous.Add(state))
            {
                cycleLength = i + 1;

                break;
            }
        }

        for (var i = 1_000_000_000 / cycleLength * cycleLength; i < 1_000_000_000; i++)
        {
            RunDance(ref dancers, Input[0]);
        }

        return new string(dancers);
    }
}