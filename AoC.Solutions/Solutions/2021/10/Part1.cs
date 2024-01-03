using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._10;

[UsedImplicitly]
public class Part1 : Base
{
    private static readonly Dictionary<char, int> Scores = new()
                                                           {
                                                               { ')', 3 },
                                                               { ']', 57 },
                                                               { '}', 1197 },
                                                               { '>', 25137 }
                                                           };

    public override string GetAnswer()
    {
        var score = 0;

        foreach (var input in Input)
        {
            var isCorrupt = IsCorrupt(input);

            if (isCorrupt != '\0')
            {
                score += Scores[isCorrupt];
            }
        }

        return score.ToString();
    }
}