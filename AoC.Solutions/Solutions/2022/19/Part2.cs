using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2022._19;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput(new Range(0, 3));

        var answer = Simulate(32);

        return (answer[0].Best * answer[1].Best * answer[2].Best).ToString();
    }}