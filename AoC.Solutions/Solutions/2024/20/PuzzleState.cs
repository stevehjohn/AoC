using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2024._20;

public class PuzzleState
{
    public const int Size = 141;

    public static char[,] Map { get; private set; }

    public Point2D ShortcutStart { get; }
    
    public Point2D ShortcutEnd { get; }

    public static IReadOnlyList<Point2D> Track => _track;

    private static List<Point2D> _track;

    public PuzzleState(char[,] map, State[] state, Point2D shortcutStart, Point2D shortcutEnd)
    {
        if (Map == null)
        {
            _track = state.Select(c => c.Position).ToList();

            _track.Reverse();

            Map = map;
        }

        ShortcutStart = shortcutStart;

        ShortcutEnd = shortcutEnd;
    }
}