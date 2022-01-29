using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;
using System.Text;

namespace AoC.Solutions.Solutions._2020._20;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly List<Tile> _tiles = new();

    private Dictionary<Point, Tile> _jigsaw;

    private readonly IVisualiser<PuzzleState> _visualiser;

    public Part1()
    {
    }

    public Part1(IVisualiser<PuzzleState> visualiser)
    {
        _visualiser = visualiser;
    }

    protected void Visualise(int tileId, string transform)
    {
        if (_visualiser != null)
        {
            _visualiser.PuzzleStateChanged(new PuzzleState { Tiles = _tiles.Select(t => t.Id).ToList(), Jigsaw = _jigsaw.ToDictionary(kvp => kvp.Value.Id, kvp => kvp.Key), TileId = tileId, Transform = transform });
        }
    }

    public override string GetAnswer()
    {
#if DEBUG && DUMP
        Console.Clear();

        Console.CursorVisible = false;
#endif

        ParseInput();

        Solve();

        var result = CalculateAnswer();

        SaveResultForPart2();

        return result.ToString();
    }

    private void SaveResultForPart2()
    {
        var yMin = _jigsaw.Min(t => t.Key.Y);

        var xMin = _jigsaw.Min(t => t.Key.X);

        var lines = new List<string>();

        for (var y = yMin; y <= _jigsaw.Max(t => t.Key.Y); y++)
        {
            for (var ty = 1; ty < 9; ty++)
            {
                var builder = new StringBuilder();

                for (var x = xMin; x <= _jigsaw.Max(t => t.Key.X); x++)
                {
                    var tile = _jigsaw[new Point(x, y)];

                    for (var tx = 1; tx < 9; tx++)
                    {
                        builder.Append(tile.Image[tx, ty]);
                    }
                }

                lines.Add(builder.ToString());
            }
        }

        File.WriteAllLines(Part1ResultFile, lines);
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

#if DUMP && DEBUG
        Visualiser.Dump(_jigsaw, _tiles);
#endif

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

#if DUMP && DEBUG
            Visualiser.Dump(_jigsaw, _tiles);
#endif
        }
    }

    private bool FindTileMatch(KeyValuePair<Point, Tile> tile, int edge, int x, int y)
    {
        var match = _tiles.SingleOrDefault(t => t.Edges.Contains(edge));

        if (match == null)
        {
            return false;
        }

#if DUMP && DEBUG
        Visualiser.HighlightMatch(tile, match, _tiles, _jigsaw);
#endif

        _jigsaw.Add(new Point(x, y), match);

        _tiles.Remove(match);

        var count = 0;

        var transform = new StringBuilder();

        while (count < 4 && tile.Value.Left != match.Right && tile.Value.Right != match.Left && tile.Value.Top != match.Bottom && tile.Value.Bottom != match.Top)
        {
            match.Rotate();

            transform.Append('R');

            if (tile.Value.Left != match.Right && tile.Value.Right != match.Left && tile.Value.Top != match.Bottom && tile.Value.Bottom != match.Top)
            {
                match.FlipHorizontal();

                transform.Append('H');
            }

            if (tile.Value.Left != match.Right && tile.Value.Right != match.Left && tile.Value.Top != match.Bottom && tile.Value.Bottom != match.Top)
            {
                match.FlipHorizontal();

                transform.Remove(transform.Length - 1, 1);

                match.FlipVertical();

                transform.Append('V');
            }

            if (tile.Value.Left != match.Right && tile.Value.Right != match.Left && tile.Value.Top != match.Bottom && tile.Value.Bottom != match.Top)
            {
                match.FlipVertical();

                transform.Remove(transform.Length - 1, 1);
            }

            count++;
        }

        Visualise(match.Id, transform.ToString());

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