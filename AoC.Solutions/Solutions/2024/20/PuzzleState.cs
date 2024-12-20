using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2024._20;

public class PuzzleState
{
    private static List<Point2D> _track;

    public PuzzleState(State[] state)
    {
        _track = state.Select(c => c.Position).ToList();

        _track.Reverse();
    }
}