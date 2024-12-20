using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2024._20;

public class PuzzleState
{
    public const int Size = 141;

    public static char[,] Map => _map;
    
    public Point2D ShortcutStart { get; }
    
    public Point2D ShortcutEnd { get; }

    public static IReadOnlyList<Point2D> Track => _track;

    private static char[,] _map;
    
    private static List<Point2D> _track;

    public PuzzleState(char[,] map, State[] state, Point2D shortcutStart, Point2D shortcutEnd)
    {
        if (_map == null)
        {
            _track = state.Select(c => c.Position).ToList();

            _track.Reverse();

            _map = map;
        }

        ShortcutStart = shortcutStart;

        ShortcutEnd = shortcutEnd;
    }
}