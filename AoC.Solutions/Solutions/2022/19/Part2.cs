using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2022._19;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        Input = Input[..3];

        ParseInput();

        var answer = Simulate(32);

        return (answer[0].Best * answer[1].Best * answer[2].Best).ToString();
    }}