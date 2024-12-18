using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._18;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        WalkMaze();
        
        var result = 0;
        
        var i = 1_023;

        var offset = new Point(1, 1);
        
        while (result != -1)
        {
            i++;

            result = WalkMaze(new Point(Input[i]) + offset);
        }
        
        return Input[i];
    }
}