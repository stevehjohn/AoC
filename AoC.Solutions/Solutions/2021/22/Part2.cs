using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._22;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        ProcessInput(420);

        return GetVolume().ToString();
    }
}