namespace AoC.Solutions.Solutions._2023._22;

public class PuzzleState
{
    public int[,,] Map { get; }

    public PuzzleState(int[,,] map, int height)
    {
        Map = new int[height, 10, 10];
        
        Array.Copy(map, Map, height * 100);
    }
}