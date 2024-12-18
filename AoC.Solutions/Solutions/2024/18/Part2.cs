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

        var steps = _visualiser == null ? -1 : 0;

        var i = _visualiser == null ? Input.Length : 1_023;

        var offset = new Point2D(1, 1);
        
        Visualise();
        
        while (_visualiser == null ? steps == -1 : steps != -1)
        {
            i += _visualiser == null ? -1 : 1;

            var point = new Point2D(Input[i]) + offset;

            Map[point.X, point.Y] = _visualiser == null ? '.' : '#';
            
            var result = WalkMaze();

            steps = result.Steps;

            if (result.Steps != -1)
            {
                Visualise(result, point);
            }
        }

        return Input[i];
    }

    private void Visualise(State state = null, Point2D newPoint = default)
    {
        _visualiser?.PuzzleStateChanged(new PuzzleState(Input, state, newPoint));
    }
}