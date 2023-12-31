namespace AoC.Solutions.Solutions._2022._02;

public class Part1 : Base
{
    public override string GetAnswer()
    {
        var score = 0;

        foreach (var line in Input)
        {
            score += line[2] - 'W';

            switch (line)
            {
                case "A X":
                case "B Y":
                case "C Z":
                    score += 3;
                    break;
                case "A Y":
                case "B Z":
                case "C X":
                    score += 6;
                    break;
            }
        }

        return score.ToString();
    }
}