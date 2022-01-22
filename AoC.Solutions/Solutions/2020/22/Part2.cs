using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._22;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        Play();

        var score = CalculateScore();

        return score.ToString();
    }

    private void Play()
    {
    }
}