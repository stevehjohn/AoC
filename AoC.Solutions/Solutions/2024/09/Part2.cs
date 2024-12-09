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
        
        var result = CalculateChecksum();
        
        return result.ToString();
    }

    private void Defragment()
    {
        var fileId = FileSystem[Size - 1];

        var fileSize = 1;

        for (var i = Size - 2; i >= 0; i--)
        {
            if (FileSystem[i] == fileId)
            {
                fileSize++;
                
                continue;
            }

            TryRelocateFile(fileId, i + 1, fileSize);

            fileId = FileSystem[i];
            
            fileSize = 1;

            while (FileSystem[i] == -1)
            {
                i--;
            }

            fileId = FileSystem[i];
        }
    }

    private void TryRelocateFile(int id, int position, int size)
    {
        var freeSpaceIndex = 0;
        
        while (FileSystem[freeSpaceIndex] >= 0 && freeSpaceIndex < Size)
        {
            freeSpaceIndex++;
        }

        if (freeSpaceIndex == Size)
        {
            return;
        }

        var freeSize = 0;
        
        Console.WriteLine($"Id: {id}, Size: {size}, Position: {position}");

        while (FileSystem[freeSpaceIndex] == -1 && freeSpaceIndex < Size)
        {
            freeSpaceIndex++;

            freeSize++;
        }
        
        Console.WriteLine($"Space Index: {freeSpaceIndex - freeSize}, Size: {freeSize}");

        if (freeSize <= size)
        {
            for (var i = freeSpaceIndex - freeSize; i < freeSpaceIndex; i++)
            {
            }
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