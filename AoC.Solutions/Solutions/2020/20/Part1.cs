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
        ParseInput();

        Solve();

        return "TESTING";
    }

    private void Solve()
    {
        var tileCount = _tiles.Count;

        _jigsaw = new Dictionary<Point, Tile>
                  {
                      { new Point(), _tiles[0] }
                  };

        _tiles.RemoveAt(0);

        while (_jigsaw.Count < tileCount)
        {
            foreach (var tile in _jigsaw)
            {
                if (! _jigsaw.ContainsKey(new Point(tile.Key.X, tile.Key.Y - 1)))
                {
                    if (FindTileMatch(tile.Value, tile.Value.Top, tile.Key.X, tile.Key.Y - 1))
                    {
                        break;
                    }
                }

                if (! _jigsaw.ContainsKey(new Point(tile.Key.X + 1, tile.Key.Y)))
                {
                    if (FindTileMatch(tile.Value, tile.Value.Right, tile.Key.X + 1, tile.Key.Y))
                    {
                        break;
                    }
                }

                if (! _jigsaw.ContainsKey(new Point(tile.Key.X, tile.Key.Y + 1)))
                {
                    if (FindTileMatch(tile.Value, tile.Value.Bottom, tile.Key.X, tile.Key.Y + 1))
                    {
                        break;
                    }
                }

                if (! _jigsaw.ContainsKey(new Point(tile.Key.X - 1, tile.Key.Y)))
                {
                    if (FindTileMatch(tile.Value, tile.Value.Left, tile.Key.X - 1, tile.Key.Y))
                    {
                        break;
                    }
                }
            }

            Dump();
        }
    }

    private bool FindTileMatch(Tile tile, int edge, int x, int y)
    {
        var match = _tiles.SingleOrDefault(t => t.Edges.Contains(edge));

        if (match == null)
        {
            return false;
        }

        _jigsaw.Add(new Point(x, y), match);

        _tiles.Remove(match);

        while (tile.Left != match.Right && tile.Right != match.Left && tile.Top != match.Bottom && tile.Bottom != match.Top)
        {
            match.RotateClockwise();
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

    private void Dump()
    {
        Console.CursorVisible = false;

        Console.Clear();

        var yMin = _jigsaw.Min(t => t.Key.Y);

        var xMin = _jigsaw.Min(t => t.Key.X);

        for (var y = yMin; y <= _jigsaw.Max(t => t.Key.Y); y++)
        {
            for (var x = xMin; x <= _jigsaw.Max(t => t.Key.X); x++)
            {
                if (_jigsaw.ContainsKey(new Point(x, y)))
                {
                    var tile = _jigsaw[new Point(x, y)];

                    Console.SetCursorPosition((x + Math.Abs(xMin)) * 11 + 1, (y + Math.Abs(yMin)) * 11 + 1);

                    Console.Write(tile.TopEdge);

                    for (var ty = 1; ty < tile.LeftEdge.Length - 1; ty++)
                    {
                        Console.SetCursorPosition((x + Math.Abs(xMin)) * 11 + 1, (y + Math.Abs(yMin)) * 11 + ty + 1);

                        Console.Write(tile.LeftEdge[ty]);

                        Console.ForegroundColor = ConsoleColor.DarkGray;

                        if (ty == 4)
                        {
                            Console.Write("..");

                            Console.ForegroundColor = ConsoleColor.Blue;

                            Console.Write(tile.Id);

                            Console.ForegroundColor = ConsoleColor.DarkGray;

                            Console.Write("..");
                        }
                        else
                        {
                            Console.Write("........");
                        }

                        Console.ForegroundColor = ConsoleColor.Green;

                        Console.Write(tile.RightEdge[ty]);
                    }

                    Console.SetCursorPosition((x + Math.Abs(xMin)) * 11 + 1, (y + Math.Abs(yMin)) * 11 + 10);

                    Console.Write(tile.BottomEdge);
                }
            }
        }
    }
}