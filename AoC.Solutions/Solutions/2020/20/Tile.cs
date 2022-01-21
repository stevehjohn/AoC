using System.Collections.Immutable;
using System.Text;

namespace AoC.Solutions.Solutions._2020._20;

public class Tile
{
    public int Id { get; }

    public ImmutableHashSet<int> Edges { get; }

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

        Top = data[1].GetHashCode();

        Right = right.ToString().GetHashCode();

        Bottom = data[10].GetHashCode();

        Left = left.ToString().GetHashCode();

        TopEdge = data[1].Replace('.', ' ');

        RightEdge = right.ToString().Replace('.', ' ');

        BottomEdge = data[10].Replace('.', ' ');

        LeftEdge = left.ToString().Replace('.', ' ');

        var edges = new HashSet<int>
                    {
                        Top,
                        Right,
                        Bottom,
                        Left
                    };

        Edges = edges.ToImmutableHashSet();
    }

    public void RotateClockwise()
    {
        var sTemp = RightEdge;

        RightEdge = TopEdge;

        TopEdge = new string(LeftEdge.Reverse().ToArray());

        LeftEdge = BottomEdge;

        BottomEdge = new string(sTemp.Reverse().ToArray());

        var temp = Right;

        Right = Top;

        Top = Left;

        Left = Bottom;

        Bottom = temp;

    }
}