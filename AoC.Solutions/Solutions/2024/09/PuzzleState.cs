namespace AoC.Solutions.Solutions._2024._09;

public class PuzzleState
{
    private readonly int[] _fileSystem;

    public int this[int index] => _fileSystem[index];

    public int Length => _fileSystem.Length;
    
    public PuzzleState(int[] fileSystem)
    {
        _fileSystem = new int[fileSystem.Length];
        
        Buffer.BlockCopy(fileSystem, 0, _fileSystem, 0, fileSystem.Length * sizeof(int));
    }
}