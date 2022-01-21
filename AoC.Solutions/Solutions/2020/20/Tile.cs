using System.Collections.Immutable;
using System.Text;

namespace AoC.Solutions.Solutions._2020._20;

public class Tile
{
    public int Id { get; }

    public ImmutableHashSet<int> Edges { get; private set; }

    public int Top { get; private set; }

    public int Right { get; private set; }

    public int Bottom { get; private set; }

    public int Left { get; private set; }

    public string TopEdge { get; private set; }

    public string RightEdge { get; private set; }

    public string BottomEdge { get; private set; }

    public string LeftEdge { get; private set; }

    public Tile(List<string> data)
    {
        Id = int.Parse(data[0].Substring(5, 4));

        var left = new StringBuilder();

        var right = new StringBuilder();

        for (var y = 1; y < 11; y++)
        {
            left.Append(data[y][0]);

            right.Append(data[y][9]);
        }

        TopEdge = data[1];

        RightEdge = right.ToString();

        BottomEdge = data[10];

        LeftEdge = left.ToString();

        RecalculateHashes();
    }

    public void RotateClockwise()
    {
        var temp = RightEdge;

        RightEdge = TopEdge;

        TopEdge = new string(LeftEdge.Reverse().ToArray());

        LeftEdge = BottomEdge;

        BottomEdge = new string(temp.Reverse().ToArray());

        RecalculateHashes();
    }

    public void FlipVertical()
    {
        (BottomEdge, TopEdge) = (TopEdge, BottomEdge);

        LeftEdge = new string(LeftEdge.Reverse().ToArray());

        RightEdge = new string(RightEdge.Reverse().ToArray());

        RecalculateHashes();
    }

    public void FlipHorizontal()
    {
        (RightEdge, LeftEdge) = (LeftEdge, RightEdge);

        TopEdge = new string(TopEdge.Reverse().ToArray());

        BottomEdge = new string(BottomEdge.Reverse().ToArray());

        RecalculateHashes();
    }

    private void RecalculateHashes()
    {
        Right = RightEdge.GetHashCode();

        Top = TopEdge.GetHashCode();

        Left = LeftEdge.GetHashCode();

        Bottom = BottomEdge.GetHashCode();

        var edges = new HashSet<int>
                    {
                        Top,
                        Right,
                        Bottom,
                        Left,
                        new string(TopEdge.Reverse().ToArray()).GetHashCode(),
                        new string(RightEdge.Reverse().ToArray()).GetHashCode(),
                        new string(BottomEdge.Reverse().ToArray()).GetHashCode(),
                        new string(LeftEdge.Reverse().ToArray()).GetHashCode()
                    };

        Edges = edges.ToImmutableHashSet();
    }
}