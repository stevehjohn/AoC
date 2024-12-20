using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._20;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var baseTime = Race();

        var count = 0;

        for (var y = 1; y < Height - 1; y++)
        {
            for (var x = 1; x < Width - 1; x++)
            {
                if (Map[x, y] == '.')
                {
                    continue;
                }

                Map[x, y] = '.';
                
                var result = Race();

                Map[x, y] = '#';

                if (result < baseTime - 100)
                {
                    count++;
                }
            }
            
            Console.WriteLine(y);
        }
        
        return count.ToString();
    }
}