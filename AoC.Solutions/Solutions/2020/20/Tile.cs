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

    public int TopFlipped { get; private set; }

    public int RightFlipped { get; private set; }

    public int BottomFlipped { get; private set; }

    public int LeftFlipped { get; private set; }

    public string TopEdge { get; private set; }

    public string RightEdge { get; private set; }

    public string BottomEdge { get; private set; }

    public string LeftEdge { get; private set; }

    public char[,] Image { get; private set; }

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

        var image = new char[10, 10];

        for (var y = 0; y < 10; y++)
        {
            for (var x = 0; x < 10; x++)
            {
                image[x, y] = data[y + 1][x];
            }
        }

        Image = image;
    }

    public void Rotate()
    {
        var temp = RightEdge;

        RightEdge = TopEdge;

        TopEdge = new string(LeftEdge.Reverse().ToArray());

        LeftEdge = BottomEdge;

        BottomEdge = new string(temp.Reverse().ToArray());

        RecalculateHashes();

        var image = new char[10, 10];

        for (var y = 0; y < 10; y++)
        {
            for (var x = 0; x < 10; x++)
            {
                image[x, y] = Image[y, 9 - x];
            }
        }

        Image = image;
    }

    public void FlipVertical()
    {
        (BottomEdge, TopEdge) = (TopEdge, BottomEdge);

        LeftEdge = new string(LeftEdge.Reverse().ToArray());

        RightEdge = new string(RightEdge.Reverse().ToArray());

        RecalculateHashes();

        var image = new char[10, 10];

        for (var y = 0; y < 10; y++)
        {
            for (var x = 0; x < 10; x++)
            {
                image[x, y] = Image[x, 9 - y];
            }
        }

        Image = image;
    }

    public void FlipHorizontal()
    {
        (RightEdge, LeftEdge) = (LeftEdge, RightEdge);

        TopEdge = new string(TopEdge.Reverse().ToArray());

        BottomEdge = new string(BottomEdge.Reverse().ToArray());

        RecalculateHashes();

        var image = new char[10, 10];

        for (var y = 0; y < 10; y++)
        {
            for (var x = 0; x < 10; x++)
            {
                image[x, y] = Image[9 - x, y];
            }
        }

        Image = image;
    }

    private void RecalculateHashes()
    {
        Right = RightEdge.GetHashCode();

        Top = TopEdge.GetHashCode();

        Left = LeftEdge.GetHashCode();

        Bottom = BottomEdge.GetHashCode();

        RightFlipped = new string(RightEdge.Reverse().ToArray()).GetHashCode();
        
        TopFlipped = new string(TopEdge.Reverse().ToArray()).GetHashCode();
        
        LeftFlipped = new string(LeftEdge.Reverse().ToArray()).GetHashCode();
        
        BottomFlipped = new string(BottomEdge.Reverse().ToArray()).GetHashCode();

        var edges = new HashSet<int>
                    {
                        Top,
                        Right,
                        Bottom,
                        Left,
                        TopFlipped,
                        RightFlipped,
                        BottomFlipped,
                        LeftFlipped
                    };

        Edges = edges.ToImmutableHashSet();
    }
}