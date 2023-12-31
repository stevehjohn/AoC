using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._10;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var (x, y) = ParseInput();

        var result = GetSteps(x, y);
        
        return result.ToString();
    }

    private int GetSteps(int x, int y)
    {
        if (! new[] { '-', 'L', 'J', '.' }.Contains(Map[y - 1][x]))
        {
            return WalkPipes(x, y - 1, 0, -1);
        }

        if (! new[] { '-', 'L', 'J', '.' }.Contains(Map[y + 1][x]))
        {
            return WalkPipes(x, y + 1, 0, 1);
        }

        if (! new[] { '-', 'L', 'J', '.' }.Contains(Map[y][x - 1]))
        {
            return WalkPipes(x - 1, y, -1, 0);
        }
        
        if (! new[] { '-', 'L', 'J', '.' }.Contains(Map[y][x + 1]))
        {
            return WalkPipes(x + 1, y, 1, 0);
        }
        
        return 0;
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

    private (int X, int Y) ParseInput()
    {
        Map = new char[Input.Length + 2][];

        var x = 0;

        var y = 0;

        Map[0] = new char[Input[0].Length + 2];
        
        Map[Input.Length + 1] = new char[Input[0].Length + 2];
        
        Array.Fill(Map[0], 'X');
        
        Array.Fill(Map[Input.Length + 1], 'X');
        
        for (var i = 0; i < Input.Length; i++)
        {
            Map[i + 1] = new char[Input[i].Length + 2];

            Map[i + 1][0] = 'X';
            
            Map[i + 1][Input[i].Length + 1] = 'X';
            
            for (var j = 0; j < Input[i].Length; j++)
            {
                Map[i + 1][j + 1] = Input[i][j];

                if (Input[i][j] == 'S')
                {
                    x = j + 1;

                    y = i + 1;
                }
            }
        }

        return (x, y);
    }
}