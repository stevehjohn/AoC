using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._10;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var (x, y) = ParseInput();

        var allSteps = GetAllSteps(x, y);
        
        return allSteps.Max().ToString();
    }

    private List<int> GetAllSteps(int x, int y)
    {
        var allSteps = new List<int>();

        if (! new[] { '-', 'L', 'J', '.' }.Contains(Map[y - 1][x]))
        {
            allSteps.Add(WalkPipes(x, y - 1, 0, -1));
        }

        if (! new[] { '-', 'L', 'J', '.' }.Contains(Map[y + 1][x]))
        {
            allSteps.Add(WalkPipes(x, y + 1, 0, 1));
        }

        if (! new[] { '-', 'L', 'J', '.' }.Contains(Map[y][x - 1]))
        {
            allSteps.Add(WalkPipes(x - 1, y, -1, 0));
        }
        
        if (! new[] { '-', 'L', 'J', '.' }.Contains(Map[y][x + 1]))
        {
            allSteps.Add(WalkPipes(x + 1, y, 1, 0));
        }
        
        return allSteps;
    }

    private int WalkPipes(int startX, int startY, int dX, int dY)
    {
        var steps = 1;

        var x = startX;

        var y = startY;

        while (Map[y][x] != 'S')
        {
            steps++;

            switch (Map[y][x], dX, dY)
            {
                case ('F', _, -1):
                    dX = 1;
                    dY = 0;
                    break;
                
                case ('F', -1, _):
                    dX = 0;
                    dY = 1;
                    break;
                
                case ('J', 1, _):
                    dX = 0;
                    dY = -1;
                    break;
                
                case ('J', _, 1):
                    dX = -1;
                    dY = 0;
                    break;
                
                case ('L', -1, _):
                    dX = 0;
                    dY = -1;
                    break;
                
                case ('L', _, 1):
                    dX = 1;
                    dY = 0;
                    break;
                
                case ('7', _, -1):
                    dX = -1;
                    dY = 0;
                    break;
                
                case ('7', 1, _):
                    dX = 0;
                    dY = 1;
                    break;
            }

            x += dX;

            y += dY;
        }

        return steps / 2;
    }
}