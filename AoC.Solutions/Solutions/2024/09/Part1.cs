using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._09;

[UsedImplicitly]
public class Part1 : Base
{
    private string _fileMap;
    
    private int[] _fileSystem;

    private int _size;
    
    public override string GetAnswer()
    {
        _fileMap = Input[0];

        CalculateRequiredSize();

        IdentifyFiles();
        
        Dump();
        
        return "Unknown";
    }

    private void Dump()
    {
        for (var i = 0; i < _fileSystem.Length; i++)
        {
            Console.Write(_fileSystem[i] == -1 ? '.' : _fileSystem[i].ToString());
        }
        
        Console.WriteLine();
    }

    private void IdentifyFiles()
    {
        Array.Fill(_fileSystem, -1);

        var id = 0;

        var position = 0;

        for (var i = 0; i < _fileMap.Length; i++)
        {
            var length = _fileMap[i] - '0';

            for (var j = 0; j < length; j++)
            {
                _fileSystem[position] = id;

                position++;
            }

            if (i < _fileMap.Length - 1)
            {
                position += _fileMap[i] - '0';

                position++;
                
                i++;
            }

            id++;
        }
    }

    private void CalculateRequiredSize()
    {
        for (var i = 0; i < _fileMap.Length; i++)
        {
            _size += _fileMap[i] - '0';
        }

        _fileSystem = new int[_size];
    }
}