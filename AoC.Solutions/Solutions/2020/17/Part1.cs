using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._17;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        for (var i = 0; i < 6; i++)
        {
            RunCycle();
        }

        return ActiveCubes.Count.ToString();
    }
}