using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._09;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        CalculateRequiredSize();

        IdentifyFiles();

        var result = CalculateChecksum();
        
        return result.ToString();
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