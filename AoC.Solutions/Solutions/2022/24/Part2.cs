using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2022._24;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var answer = RunSimulation(3);

        return answer.ToString();
    }
}