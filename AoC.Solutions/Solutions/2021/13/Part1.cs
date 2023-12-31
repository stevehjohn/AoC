using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._13;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        DoFolds(true);

        return Dots.Count.ToString();
    }
}