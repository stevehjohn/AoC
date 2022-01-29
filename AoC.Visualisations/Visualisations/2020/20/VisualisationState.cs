using Point = Microsoft.Xna.Framework.Point;

namespace AoC.Visualisations.Visualisations._2020._20;

public class VisualisationState
{
    public Mode Mode { get; set; }

    public int Elapsed { get; set; }

    public Point ScannerPosition { get; set; }
}

public enum Mode
{
    Scanning,
    HighlightMatch,
    MoveToCentre,
    Transform,
    MoveToJigsaw
}