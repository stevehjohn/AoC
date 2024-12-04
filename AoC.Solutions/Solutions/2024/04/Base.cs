using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._04;

public abstract class Base : Solution
{
    public override string Description => "Ceres search";

    protected readonly (int Left, int Up)[] Directions = [(-1, 0), (0, -1), (1, 0), (0, 1), (-1, -1), (-1, 1), (1, -1), (1, 1)];

    protected int Width;

    protected int Height;
    
    protected int ScanPuzzle()
    {
        Width = Input[0].Length;

        Height = Input.Length;
        
        var count = 0;
        
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                count += CheckCell(x, y);
            }
        }

        return count;
    }

    protected abstract int CheckCell(int x, int y);
}