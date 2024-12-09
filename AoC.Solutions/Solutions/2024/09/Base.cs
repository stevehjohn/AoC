using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._09;

public abstract class Base : Solution
{
    public override string Description => "Disk fragmenter";

    private readonly string _fileMap;
    
    private readonly IVisualiser<PuzzleState> _visualiser;
    
    protected int[] FileSystem;

    protected int Size;

    protected Base()
    {
        _fileMap = Input[0];
    }
    
    protected Base(IVisualiser<PuzzleState> visualiser)
    {
        _fileMap = Input[0];

        _visualiser = visualiser;
    }

    protected void Visualise()
    {
        _visualiser?.PuzzleStateChanged(new PuzzleState(FileSystem));
    }

    protected long CalculateChecksum(bool contiguous = true)
    {
        var checksum = 0L;
        
        for (var i = 0; i < FileSystem.Length; i++)
        {
            if (FileSystem[i] == -1)
            {
                if (contiguous)
                {
                    break;
                }
                
                continue;
            }

            checksum += i * FileSystem[i];
        }

        return checksum;
    }
    
    protected void IdentifyFiles()
    {
        Array.Fill(FileSystem, -1);

        var id = 0;

        var position = 0;

        for (var i = 0; i < _fileMap.Length; i++)
        {
            var length = _fileMap[i] - '0';

            for (var j = 0; j < length; j++)
            {
                FileSystem[position] = id;

                position++;
            }

            if (i < _fileMap.Length - 1)
            {
                i++;

                position += _fileMap[i] - '0';
            }

            id++;
        }
    }

    protected void CalculateRequiredSize()
    {
        for (var i = 0; i < _fileMap.Length; i++)
        {
            Size += _fileMap[i] - '0';
        }

        FileSystem = new int[Size];
    }
}