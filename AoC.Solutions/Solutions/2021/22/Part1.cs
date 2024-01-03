using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._22;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        ProcessInput(20);

        return GetVolume().ToString();
    }
}