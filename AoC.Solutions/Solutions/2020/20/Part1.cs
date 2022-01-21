using AoC.Solutions.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._20;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly List<Tile> _tiles = new();

    private Dictionary<Point, Tile> _jigsaw;

    public override string GetAnswer()
    {
        Console.Clear();

        Console.CursorVisible = false;

        ParseInput();

        Solve();

        var result = CalculateAnswer();

        return result.ToString();
    }

    private long CalculateAnswer()
    {
        var minX = _jigsaw.Min(t => t.Key.X);

        var maxX = _jigsaw.Max(t => t.Key.X);

        var minY = _jigsaw.Min(t => t.Key.Y);

        var maxY = _jigsaw.Max(t => t.Key.Y);

        var answer = (long) _jigsaw.Single(j => j.Key.X == minX && j.Key.Y == minY).Value.Id;

        answer *= _jigsaw.Single(j => j.Key.X == minX && j.Key.Y == maxY).Value.Id;

        answer *= _jigsaw.Single(j => j.Key.X == maxX && j.Key.Y == minY).Value.Id;

        answer *= _jigsaw.Single(j => j.Key.X == maxX && j.Key.Y == maxY).Value.Id;

        return answer;
    }

    private void Solve()
    {
        var tileCount = _tiles.Count;

        _jigsaw = new Dictionary<Point, Tile>
                  {
                      { new Point(), _tiles[0] }
                  };

        _tiles.RemoveAt(0);

        Visualiser.Dump(_jigsaw, _tiles);

        while (_jigsaw.Count < tileCount)
        {
            foreach (var tile in _jigsaw)
            {
                if (! _jigsaw.ContainsKey(new Point(tile.Key.X, tile.Key.Y - 1)))
                {
                    if (FindTileMatch(tile, tile.Value.Top, tile.Key.X, tile.Key.Y - 1))
                    {
                        break;
                    }
                }

                if (! _jigsaw.ContainsKey(new Point(tile.Key.X + 1, tile.Key.Y)))
                {
                    if (FindTileMatch(tile, tile.Value.Right, tile.Key.X + 1, tile.Key.Y))
                    {
                        break;
                    }
                }

                if (! _jigsaw.ContainsKey(new Point(tile.Key.X, tile.Key.Y + 1)))
                {
                    if (FindTileMatch(tile, tile.Value.Bottom, tile.Key.X, tile.Key.Y + 1))
                    {
                        break;
                    }
                }

                if (! _jigsaw.ContainsKey(new Point(tile.Key.X - 1, tile.Key.Y)))
                {
                    if (FindTileMatch(tile, tile.Value.Left, tile.Key.X - 1, tile.Key.Y))
                    {
                        break;
                    }
                }
            }

            Visualiser.Dump(_jigsaw, _tiles);
        }
    }

    private bool FindTileMatch(KeyValuePair<Point, Tile> tile, int edge, int x, int y)
    {
        var match = _tiles.SingleOrDefault(t => t.Edges.Contains(edge));

        if (match == null)
        {
            return false;
        }

        Visualiser.HighlightMatch(tile, match, _tiles, _jigsaw);

        _jigsaw.Add(new Point(x, y), match);

        _tiles.Remove(match);

        var count = 0;

        while (count < 4 && tile.Value.Left != match.Right && tile.Value.Right != match.Left && tile.Value.Top != match.Bottom && tile.Value.Bottom != match.Top)
        {
            match.RotateClockwise();

            if (tile.Value.Left != match.Right && tile.Value.Right != match.Left && tile.Value.Top != match.Bottom && tile.Value.Bottom != match.Top)
            {
                match.FlipHorizontal();
            }

            if (tile.Value.Left != match.Right && tile.Value.Right != match.Left && tile.Value.Top != match.Bottom && tile.Value.Bottom != match.Top)
            {
                match.FlipHorizontal();

                match.FlipVertical();
            }

            if (tile.Value.Left != match.Right && tile.Value.Right != match.Left && tile.Value.Top != match.Bottom && tile.Value.Bottom != match.Top)
            {
                match.FlipVertical();
            }

            count++;
        }

        return true;
    }

    private void ParseInput()
    {
        var tileLines = new List<string>();

        foreach (var line in Input)
        {
            if (! string.IsNullOrWhiteSpace(line))
            {
                tileLines.Add(line);

                continue;
            }

            _tiles.Add(new Tile(tileLines));

            tileLines.Clear();
        }

        _tiles.Add(new Tile(tileLines));
    }
}