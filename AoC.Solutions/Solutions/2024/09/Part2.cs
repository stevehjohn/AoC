using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._09;

[UsedImplicitly]
public class Part2 : Base
{
    private readonly List<(int Position, int Size)> _freeSpace = [];
    
    public override string GetAnswer()
    {
        CalculateRequiredSize();

        IdentifyFiles();
        
        MapFreeSpace();
        
        Defragment();
        
        var result = CalculateChecksum(false);
        
        return result.ToString();
    }

    private void Defragment()
    {
        var fileId = FileSystem[Size - 1];

        while (fileId > 0)
        {
            var position = Array.IndexOf(FileSystem, fileId);

            var size = 1;

            while (position + size < Size && FileSystem[position + size] == fileId)
            {
                size++;
            }

            TryRelocateFile(position, size);

            fileId--;
        }
    }

    private void MapFreeSpace()
    {
        var freeSize = 0;

        for (var i = 0; i < Size; i++)
        {
            if (FileSystem[i] >= 0)
            {
                continue;
            }

            while (FileSystem[i] == -1)
            {
                freeSize++;

                i++;

                if (i == Size)
                {
                    return;
                }
            }
            
            _freeSpace.Add((i - freeSize, freeSize));

            freeSize = 0;
        }
    }

    private void TryRelocateFile(int position, int size)
    {
        for (var i = 0; i < _freeSpace.Count; i++)
        {
            var block = _freeSpace[i];

            if (block.Position > position)
            {
                return;
            }

            if (block.Size >= size)
            {
                for (var j = 0; j < size; j++)
                {
                    FileSystem[block.Position + j] = FileSystem[position + j];

                    FileSystem[position + j] = -1;
                }

                if (block.Size == size)
                {
                    _freeSpace.RemoveAt(i);
                }
                else
                {
                    _freeSpace[i] = (block.Position + size, block.Size - size);
                }

                return;
            }
        }
    }
}