using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2025._04;

public abstract class Base : Solution
{
    public override string Description => "Printing department";
    
    protected int Width;

    protected int Height;
    
    protected char[,] Map;
    
    protected char SafeCheckCell(int y, int x)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height)
        {
            return '\0';
        }

        return Map[y, x];
    }

    protected void ParseInput()
    {
        Width = Input[0].Length;

        Height = Input.Length;
        
        Map = new char[Height, Width];

        for (var y = 0; y < Height; y++)
        {
            var line = Input[y];
            
            for (var x = 0; x < Width; x++)
            {
                Map[y, x] = line[x];
            }
        }
    }
}