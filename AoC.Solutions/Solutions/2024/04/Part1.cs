using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._04;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var result = ScanPuzzle();
        
        return result.ToString();
    }

    protected override int CheckCell(int x, int y)
    {
        const string word = "XMAS";
        
        if (Input[y][x] != word[0])
        {
            return 0;
        }

        var count = 0;

        for (var d = 0; d < Directions.Length; d++)
        {
            var direction = Directions[d];

            var found = true;
            
            for (var i = 1; i < word.Length; i++)
            {
                var checkX = x + direction.Left * i;

                if (checkX < 0 || checkX >= Width)
                {
                    found = false;
                    
                    break;
                }

                var checkY = y + direction.Up * i;

                if (checkY < 0 || checkY >= Height)
                {
                    found = false;
                    
                    break;
                }

                if (Input[checkY][checkX] != word[i])
                {
                    found = false;
                    
                    break;
                }
            }

            if (found)
            {
                count++;
            }
        }

        return count;
    }
}