using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._09;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        CalculateRequiredSize();

        IdentifyFiles();

        Defragment();

        var result = CalculateChecksum();
        
        return result.ToString();
    }

    private void Defragment()
    {
        var target = 0;
        
        for (var i = FileSystem.Length - 1; i >= 0; i--)
        {
            if (FileSystem[i] == -1)
            {
                continue;
            }

            while (FileSystem[target] >= 0)
            {
                target++;
            }

            if (target >= i)
            {
                break;
            }

            FileSystem[target] = FileSystem[i];

            FileSystem[i] = -1;
        }
    }
}