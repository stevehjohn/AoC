using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._23;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();
        
        var result = Solve();
        
        return result.ToString();
    }

    private void Dump()
    {
        for (var x = 1; x < Width - 1; x++)
        {
            for (var y = 1; y < Height - 1; y++)
            {
                Console.Write(Map[x, y]);
            }
            
            Console.WriteLine();
        }
    }
}