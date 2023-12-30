namespace AoC.Solutions.Solutions._2023._22;

public class PuzzleState
{
    public int[,,] Map { get; }

    public int Height { get; }

    public PuzzleState(int[,,] map, int height)
    {
        Map = new int[height, 10, 10];

        Height = height;
        
        Array.Copy(map, Map, height * 100);
    }
}