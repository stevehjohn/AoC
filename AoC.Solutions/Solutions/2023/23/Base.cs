using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._23;

public abstract class Base : Solution
{
    public override string Description => "A long walk";

    protected char[,] Map;

    protected int Width;

    protected int Height;
    
    protected static readonly (int, int) North = (0, -1);
    
    protected static readonly (int, int) East = (1, 0);
    
    protected static readonly (int, int) South = (0, 1);
    
    protected static readonly (int, int) West = (-1, 0);
    
    protected void ParseInput()
    {
        Map = Input.To2DArray();

        Width = Map.GetLength(0);

        Height = Map.GetLength(1);
    }
}