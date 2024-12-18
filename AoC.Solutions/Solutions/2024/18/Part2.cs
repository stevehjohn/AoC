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
        ParseInput(_visualiser == null ? int.MaxValue : 1_024);

        WalkMaze();

        var result = _visualiser == null ? -1 : 0;

        var i = _visualiser == null ? Input.Length : 1_023;

        var offset = new Point2D(1, 1);
        
        while (_visualiser == null ? result == -1 : result != -1)
        {
            i += _visualiser == null ? -1 : 1;

            var point = new Point2D(Input[i]) + offset;

            Map[point.X, point.Y] = _visualiser == null ? '.' : '#';
            
            result = WalkMaze();
        }
        
        return Input[i];
    }

    private void Visualise()
    {
        _visualiser?.PuzzleStateChanged(new PuzzleState(Input));
    }
}