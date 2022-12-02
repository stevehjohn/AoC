namespace AoC.Solutions.Solutions._2022._02;

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
                    switch (line[0])
                    {
                        case 'A':
                            score += 3;
                            break;
                        case 'B':
                            score++;
                            break;
                        case 'C':
                            score += 2;
                            break;
                    }
                    break;
                case 'Y':
                    score += line[0] - '@' + 3;
                    break;
                case 'Z':
                    switch (line[0])
                    {
                        case 'A':
                            score += 2;
                            break;
                        case 'B':
                            score += 3;
                            break;
                        case 'C':
                            score++;
                            break;
                    }
                    score += 6;
                    break;
            }
        }

        return score.ToString();
    }
}