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
            if (FileSystem[i] == -1)
            {
                continue;
            }

            if (FileSystem[i] == fileId)
            {
                fileSize++;
                
                continue;
            }
            
            TryRelocateFile(fileId, fileSize);

            fileId = FileSystem[i];
            
            fileSize = 1;
        }
            
        TryRelocateFile(fileId, fileSize);
    }

    private void TryRelocateFile(int id, int size)
    {
        Console.WriteLine($"Id: {id}, Size: {size}");
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