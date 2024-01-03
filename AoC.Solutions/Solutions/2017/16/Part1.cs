using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._16;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var dancers = new char[16];

        for (var i = 0; i < 16; i++)
        {
            dancers[i] = (char) ('a' + i);
        }

        RunDance(ref dancers, Input[0]);

        return new string(dancers);
    }
}