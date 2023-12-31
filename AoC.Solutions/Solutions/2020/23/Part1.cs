using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._23;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        for (var i = 0; i < 100; i++)
        {
            PerformMove();
        }

        return GetCupsFromOne();
    }

    private string GetCupsFromOne()
    {
        var cups = new char[Cups.Length - 2];

        var cup = 1;

        for (var i = 0; i < Cups.Length - 2; i++)
        {
            cups[i] = (char) (Cups[cup] + '0');

            cup = Cups[cup];
        }

        return new string(cups);
    }
}