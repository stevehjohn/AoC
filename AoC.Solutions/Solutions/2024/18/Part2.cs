using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._18;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        WalkMaze();
        
        var result = -1;

        var i = Input.Length - 1;

        var offset = new Point(1, 1);
        
        while (result == -1)
        {
            var point = new Point(Input[i]) + offset;

            Map[point.X, point.Y] = '.';
            
            result = WalkMaze();

            i--;
        }
        
        return Input[i + 1];
    }
}