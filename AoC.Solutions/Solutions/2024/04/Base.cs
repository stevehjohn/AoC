using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._04;

public abstract class Base : Solution
{
    public override string Description => "Ceres search";

    private readonly (int Left, int Up)[] _directions = [(-1, 0), (0, -1), (1, 0), (0, 1), (-1, -1), (-1, 1), (1, -1), (1, 1)];

    private int _width;

    private int _height;
    
    protected int ScanPuzzle()
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

            var found = true;
            
            for (var i = 1; i < word.Length; i++)
            {
                var checkX = x + direction.Left * i;

                if (checkX < 0 || checkX >= _width)
                {
                    found = false;
                    
                    break;
                }

                var checkY = y + direction.Up * i;

                if (checkY < 0 || checkY >= _height)
                {
                    found = false;
                    
                    break;
                }

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