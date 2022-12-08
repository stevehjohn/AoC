namespace AoC.Solutions.Solutions._2022._08;

public class Part2 : Base
{
    public override string GetAnswer()
    {
        ProcessInput();

        var result = GetScenicScore();

        return result.ToString();
    }

    private int GetScenicScore()
    {
        var maxScore = 0;

        for (var y = 0; y < Size; y++)
        {
            for (var x = 0; x < Size; x++)
            {
                var score = GetAxisScore(x, y, -1, 0)
                            * GetAxisScore(x, y, 1, 0)
                            * GetAxisScore(x, y, 0, -1)
                            * GetAxisScore(x, y, 0, 1);

                if (score > maxScore)
                {
                    maxScore = score;
                }
            }
        }

        return maxScore;
    }

    private int GetAxisScore(int x, int y, int dx, int dy)
    {
        if (x <= 0 || x >= Size - 1 || y <= 0 || y >= Size - 1)
        {
            return 0;
        }

        var tree = Matrix[x, y];

        var count = 0;

        do
        {
            x += dx;

            y += dy;

            count++;

            if (Matrix[x, y] >= tree)
            {
                break;
            }
        } while (x > 0 && x < Size - 1 && y > 0 && y < Size - 1);

        return count;
    }
}