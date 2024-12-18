using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._18;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var result = 0;
        
        var i = 1_024;

        while (result != -1)
        {
            var parts = Input[i].Split(',');

            Map[int.Parse(parts[0]) + 1, int.Parse(parts[1]) + 1] = '#';
            
            result = WalkMaze();

            i++;
        }
        
        return Input[i - 1];
    }
}