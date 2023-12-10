using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._10;

[UsedImplicitly]
public class Part1 : Base
{
    private char[][] _map;
    
    public override string GetAnswer()
    {
        var (x, y) = ParseInput();

        var allSteps = GetAllSteps(x, y);
        
        return allSteps.Max().ToString();
    }

    private List<int> GetAllSteps(int x, int y)
    {
        var allSteps = new List<int>();

        if (! new[] { '-', 'L', 'J', '.' }.Contains(_map[y - 1][x]))
        {
            allSteps.Add(WalkPipes(x, y - 1, 0, -1));
        }

        if (! new[] { '-', 'L', 'J', '.' }.Contains(_map[y + 1][x]))
        {
            allSteps.Add(WalkPipes(x, y + 1, 0, 1));
        }

        if (! new[] { '-', 'L', 'J', '.' }.Contains(_map[y][x - 1]))
        {
            allSteps.Add(WalkPipes(x - 1, y, -1, 0));
        }
        
        if (! new[] { '-', 'L', 'J', '.' }.Contains(_map[y][x + 1]))
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

        while (_map[y][x] != 'S')
        {
            steps++;

            switch (_map[y][x], dX, dY)
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
            
            // var yy = 0;
            // foreach (var line in _map)
            // {
            //     var xx = 0;
            //     foreach (var item in line)
            //     {
            //         if (y == yy && x == xx)
            //         {
            //             Console.Write('*');
            //         }
            //         else
            //         {
            //             Console.Write(item);
            //         }
            //
            //         xx++;
            //     }
            //
            //     yy++;
            //     
            //     Console.WriteLine();
            // }
            // Console.WriteLine($"{steps} - {Math.Abs(x - startX + (y - startY))}");
        }

        return steps / 2;
    }

    private (int X, int Y) ParseInput()
    {
        _map = new char[Input.Length + 2][];

        var x = 0;

        var y = 0;

        _map[0] = new char[Input[0].Length + 2];
        _map[Input.Length + 1] = new char[Input[0].Length + 2];
        
        Array.Fill(_map[0], '.');
        Array.Fill(_map[Input.Length + 1], '.');
        
        for (var i = 0; i < Input.Length; i++)
        {
            _map[i + 1] = new char[Input[i].Length + 2];

            _map[i + 1][0] = '.';
            _map[i + 1][Input[i].Length + 1] = '.';
            
            for (var j = 0; j < Input[i].Length; j++)
            {
                _map[i + 1][j + 1] = Input[i][j];

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