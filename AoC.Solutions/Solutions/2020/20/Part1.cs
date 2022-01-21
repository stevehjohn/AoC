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

        Dump();

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

            Dump();
        }
    }

    private bool FindTileMatch(KeyValuePair<Point, Tile> tile, int edge, int x, int y)
    {
        var match = _tiles.SingleOrDefault(t => t.Edges.Contains(edge));

        if (match == null)
        {
            return false;
        }

        HighlightMatch(tile, match);

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

    private void HighlightMatch(KeyValuePair<Point, Tile> jigsaw, Tile queue)
    {
        var i = _tiles.IndexOf(queue);

        var x = 150;

        var y = 1;

        while (i > 0)
        {
            x += 11;

            if (x > 280)
            {
                x = 150;

                y += 11;
            }

            i--;
        }

        Console.BackgroundColor = ConsoleColor.DarkBlue;

        DumpTile(queue, x, y);

        var yMin = _jigsaw.Min(t => t.Key.Y);

        var xMin = _jigsaw.Min(t => t.Key.X);

        DumpTile(jigsaw.Value, (jigsaw.Key.X + Math.Abs(xMin)) * 11 + 1, (jigsaw.Key.Y + Math.Abs(yMin)) * 11 + 1);

        for (var f = 0; f < 3; f++)
        {
            HighlightEdge(queue, x, y, jigsaw.Value.Edges.First(e => queue.Edges.Any(qe => qe == e)), f % 2 == 0);

            HighlightEdge(jigsaw.Value, (jigsaw.Key.X + Math.Abs(xMin)) * 11 + 1, (jigsaw.Key.Y + Math.Abs(yMin)) * 11 + 1, jigsaw.Value.Edges.First(e => queue.Edges.Any(qe => qe == e)), f % 2 == 0);

            Thread.Sleep(f % 2 == 0 ? 400 : 200);
        }

        Console.BackgroundColor = ConsoleColor.Black;
    }

    private static void HighlightEdge(Tile tile, int x, int y, int edge, bool on)
    {
        if (edge == tile.Top || edge == tile.TopFlipped)
        {
            Console.SetCursorPosition(x, y);

            Console.BackgroundColor = on ? ConsoleColor.Red : ConsoleColor.DarkBlue;

            Console.Write(tile.TopEdge);

            Console.BackgroundColor = ConsoleColor.Black;
        }

        if (edge == tile.Bottom || edge == tile.BottomFlipped)
        {
            Console.SetCursorPosition(x, y + 9);

            Console.BackgroundColor = on ? ConsoleColor.Red : ConsoleColor.DarkBlue;

            Console.Write(tile.BottomEdge);

            Console.BackgroundColor = ConsoleColor.Black;
        }

        if (edge == tile.Left || edge == tile.LeftFlipped)
        {
            for (var ty = 0; ty < 10; ty++)
            {
                Console.SetCursorPosition(x, y + ty);

                Console.BackgroundColor = on ? ConsoleColor.Red : ConsoleColor.DarkBlue;

                Console.Write(tile.LeftEdge[ty]);

                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        if (edge == tile.Right || edge == tile.RightFlipped)
        {
            for (var ty = 0; ty < 10; ty++)
            {
                Console.SetCursorPosition(x + 9, y + ty);

                Console.BackgroundColor = on ? ConsoleColor.Red : ConsoleColor.DarkBlue;

                Console.Write(tile.RightEdge[ty]);

                Console.BackgroundColor = ConsoleColor.Black;
            }
        }
    }

    private void Dump()
    {
        DumpTiles();

        var yMin = _jigsaw.Min(t => t.Key.Y);

        var xMin = _jigsaw.Min(t => t.Key.X);

        for (var y = yMin; y <= _jigsaw.Max(t => t.Key.Y); y++)
        {
            for (var x = xMin; x <= _jigsaw.Max(t => t.Key.X); x++)
            {
                if (_jigsaw.ContainsKey(new Point(x, y)))
                {
                    var tile = _jigsaw[new Point(x, y)];

                    DumpTile(tile, (x + Math.Abs(xMin)) * 11 + 1, (y + Math.Abs(yMin)) * 11 + 1);
                }
                else
                {
                    for (var ty = 0; ty < 11 - 1; ty++)
                    {
                        Console.SetCursorPosition((x + Math.Abs(xMin)) * 11 + 1, (y + Math.Abs(yMin)) * 11 + ty + 1);

                        Console.Write("          ");
                    }
                }
            }
        }

        Console.WriteLine("\n");
    }

    private void DumpTiles()
    {
        var x = 150;

        var y = 1;

        foreach (var tile in _tiles)
        {
            DumpTile(tile, x, y);

            x += 11;

            if (x > 280)
            {
                x = 150;

                y += 11;
            }
        }

        for (var ty = 0; ty < 10; ty++)
        {
            Console.SetCursorPosition(x, y + ty);

            Console.Write("          ");
        }
    }

    private static void DumpTile(Tile tile, int x, int y)
    {
        Console.SetCursorPosition(x, y);

        Console.Write(tile.TopEdge.Replace('.', ' '));

        for (var ty = 1; ty < tile.LeftEdge.Length - 1; ty++)
        {
            Console.SetCursorPosition(x, y + ty);

            Console.Write(tile.LeftEdge[ty] == '#' ? '#' : ' ');

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

            Console.Write(tile.RightEdge[ty] == '#' ? '#' : ' ');
        }

        Console.SetCursorPosition(x, y + 9);

        Console.Write(tile.BottomEdge.Replace('.', ' '));
    }
}