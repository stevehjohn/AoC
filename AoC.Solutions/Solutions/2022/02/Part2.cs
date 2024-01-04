using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2022._02;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var score = 0;

        foreach (var line in Input)
        {
            switch (line[2])
            {
                case 'X':
                    score += (line[0] - 'A' + 2) % 3 + 1;
                    break;
                case 'Y':
                    score += line[0] - '@' + 3;
                    break;
                case 'Z':
                    score += (line[0] - 'A' + 1) % 3 + 7;
                    break;
            }
        }

        return score.ToString();
    }
}