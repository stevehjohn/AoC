using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2020._20;

public class PuzzleState
{
    public List<int> Tiles { get; set; }

    public Dictionary<int, Point> Jigsaw { get; set; }

    public int TileId { get; set; }

    public string Transform { get; set; }
}