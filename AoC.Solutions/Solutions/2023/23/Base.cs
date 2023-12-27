using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._23;

public abstract class Base : Solution
{
    public override string Description => "A long walk";

    protected char[,] Map;

    protected int Width;

    protected int Height;
    
    protected void ParseInput()
    {
        Map = Input.To2DArray();

        Width = Map.GetLength(0);

        Height = Map.GetLength(1);
    }
}