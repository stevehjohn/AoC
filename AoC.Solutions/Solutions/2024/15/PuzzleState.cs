namespace AoC.Solutions.Solutions._2024._15;

public class PuzzleState
{
    public char[,] Map { get; }
    
    public int Width { get; }
    
    public int Height { get; }

    public PuzzleState(char[,] map)
    {
        Width = map.GetLength(0);

        Height = map.GetLength(1);
        
        Map = new char[Width, Height];
        
        Buffer.BlockCopy(map, 0, Map, 0, sizeof(char) * Width * Height);
    }
}