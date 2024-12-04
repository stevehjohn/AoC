using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._04;

[UsedImplicitly]
public class Part1 : Base
{

    private readonly (int Left, int Up)[] _directions = [(-1, 0), (0, -1), (1, 0), (0, 1), (-1, -1), (-1, 1), (1, -1), (1, 1)];

    private int _width;

    private int _height;
    
    public override string GetAnswer()
    {
        var result = ScanPuzzle();
        
        return result.ToString();
    }
    
    private int ScanPuzzle()
    {
        _width = Input[0].Length;

        _height = Input.Length;
        
        var count = 0;
        
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                count += CheckCell(x, y);
            }
        }

        return count;
    }

    private int CheckCell(int x, int y)
    {
        const string word = "XMAS";
        
        if (Input[y][x] != word[0])
        {
            return 0;
        }

        var count = 0;

        for (var d = 0; d < _directions.Length; d++)
        {
            var direction = _directions[d];

            if (direction.Left == -1 && x < 3 || direction.Left == 1 && x > _width - 4)
            {
                continue;
            }

            if (direction.Up == -1 && y < 3 || direction.Up == 1 && y > _height - 4)
            {
                continue;
            }

            var found = true;
            
            for (var i = 1; i < word.Length; i++)
            {
                var checkX = x + direction.Left * i;

                var checkY = y + direction.Up * i;

                if (Input[checkY][checkX] != word[i])
                {
                    found = false;
                    
                    break;
                }
            }

            if (found)
            {
                count++;
            }
        }

        return count;
    }
}