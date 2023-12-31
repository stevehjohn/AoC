using AoC.Solutions.Exceptions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._19;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var elves = int.Parse(Input[0]);

        var pow = (int) Math.Floor(Math.Log(elves) / Math.Log(3));

        var b = (int) Math.Pow(3, pow);

        if (elves - b <= b)
        {
            return (elves - b).ToString();
        }

        throw new PuzzleException("Solution not found.");
    }
}