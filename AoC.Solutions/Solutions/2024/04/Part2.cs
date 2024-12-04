using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._04;

[UsedImplicitly]
public class Part2 : Base
{
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
        
        for (var y = 1; y < _height - 1; y++)
        {
            for (var x = 1; x < _width - 1; x++)
            {
                count += CheckCell(x, y) ? 1 : 0;
            }
        }

        return count;
    }
    
    private bool CheckCell(int x, int y)
    {
        if (Input[y][x] != 'A')
        {
            return false;
        }

        var count = 0;

        count += CheckOpposingCells(x, y, -1, -1) ? 1 : 0;

        count += CheckOpposingCells(x, y, 1, -1) ? 1 : 0;

        count += CheckOpposingCells(x, y, -1, 1) ? 1 : 0;

        count += CheckOpposingCells(x, y, -1, 1) ? 1 : 0;
        
        return count == 4;
    }

    private bool CheckOpposingCells(int x, int y, int dX, int dY)
    {
        var found = Input[y - dY][x - dX] == 'M' && Input[y + dY][x + dX] == 'S';
        
        found |= Input[y - dY][x - dX] == 'S' && Input[y + dY][x + dX] == 'M';

        return found;
    }
}