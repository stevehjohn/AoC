using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._09;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        CalculateRequiredSize();

        IdentifyFiles();

        Dump();
        
        Defragment();
        
        Dump();
        
        var result = CalculateChecksum();
        
        return result.ToString();
    }

    private void Defragment()
    {
        var fileId = FileSystem[Size - 1];

        while (fileId > 0)
        {
            var position = Array.IndexOf(FileSystem, fileId);

            var size = 1;
            
            while ( position + size < Size && FileSystem[position + size] == fileId)
            {
                size++;
            }

            Console.WriteLine($"Id: {fileId} Pos: {position} Size: {size}");

            TryRelocateFile(fileId, position, size);

            fileId--;
        }
    }

    private void TryRelocateFile(int id, int position, int size)
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

            if (size <= freeSize)
            {
                var freeIndex = i - freeSize;

                if (freeIndex < position)
                {
                    Console.WriteLine($"Can move {id} to {freeIndex}");

                    for (var j = 0; j < size; j++)
                    {
                        FileSystem[freeIndex + j] = FileSystem[position + j];

                        FileSystem[position + j] = -1;
                    }

                    Dump();

                    return;
                }
            }

            freeSize = 0;
        }
    }

    private void Dump()
    {
        for (var i = 0; i < FileSystem.Length; i++)
        {
            Console.Write(FileSystem[i] == -1 ? '.' : FileSystem[i].ToString());
        }
        
        Console.WriteLine();
    }
}