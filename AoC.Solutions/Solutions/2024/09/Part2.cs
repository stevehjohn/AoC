using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._09;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        CalculateRequiredSize();

        IdentifyFiles();
        
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

    private void TryRelocateFile(int position, int size)
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
                    for (var j = 0; j < size; j++)
                    {
                        FileSystem[freeIndex + j] = FileSystem[position + j];

                        FileSystem[position + j] = -1;
                    }

                    return;
                }
            }

            freeSize = 0;
        }
    }
}