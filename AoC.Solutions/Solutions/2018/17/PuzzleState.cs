namespace AoC.Solutions.Solutions._2018._17;

public class PuzzleState
{
    public char[,] Map { get; }

    public PuzzleState(char[,] map)
    {
        Map = new char[map.GetLength(0), map.GetLength(1)];

        Buffer.BlockCopy(map, 0, Map, 0, sizeof(char) * map.GetLength(0) * map.GetLength(1));
    }
}