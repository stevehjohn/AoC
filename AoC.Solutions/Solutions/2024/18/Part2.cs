using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._18;

[UsedImplicitly]
public class Part2 : Base
{
    private readonly IVisualiser<PuzzleState> _visualiser;

    public Part2()
    {
    }

    public Part2(IVisualiser<PuzzleState> visualiser)
    {
        _visualiser = visualiser;
    }

    public override string GetAnswer()
    {
        ParseInput();

        WalkMaze();
        
        var result = -1;

        var i = Input.Length - 1;

        var offset = new Point2D(1, 1);
        
        while (result == -1)
        {
            var point = new Point2D(Input[i]) + offset;

            Map[point.X, point.Y] = '.';
            
            result = WalkMaze();

            i--;
        }
        
        return Input[i + 1];
    }

    private void Visualise()
    {
        _visualiser?.PuzzleStateChanged(new PuzzleState(Input));
    }
}