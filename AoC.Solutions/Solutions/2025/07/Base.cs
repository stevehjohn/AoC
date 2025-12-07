using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2025._07;

public abstract class Base : Solution
{
    public override string Description => "Laboratories";

    protected readonly HashSet<int> Beams = [];

    protected char[,] Map;

    protected int Height;

    protected int Width;
    
    protected void ParseInput()
    {
        Width = Input[0].Length;

        Height = Input.Length;
        
        Beams.Add(Width / 2);

        Map = Input.To2DArray();
    }
}