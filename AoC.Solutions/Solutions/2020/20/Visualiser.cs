#if DUMP && DEBUG
using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2020._20;

public static class Visualiser
{
    public static void Dump(Dictionary<Point, Tile> jigsaw, List<Tile> tiles)
    {
        DumpTiles(tiles);

        var yMin = jigsaw.Min(t => t.Key.Y);

        var xMin = jigsaw.Min(t => t.Key.X);

        for (var y = yMin; y <= jigsaw.Max(t => t.Key.Y); y++)
        {
            for (var x = xMin; x <= jigsaw.Max(t => t.Key.X); x++)
            {
                if (jigsaw.ContainsKey(new Point(x, y)))
                {
                    var tile = jigsaw[new Point(x, y)];

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

        Console.SetCursorPosition(0, 11 * (int) Math.Sqrt(jigsaw.Count) + 1);
    }

    public static void HighlightMatch(KeyValuePair<Point, Tile> jigsaw, Tile queue, List<Tile> tiles, Dictionary<Point, Tile> wholeJigsaw)
    {
        var i = tiles.IndexOf(queue);

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

        var yMin = wholeJigsaw.Min(t => t.Key.Y);

        var xMin = wholeJigsaw.Min(t => t.Key.X);

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

    private static void DumpTiles(List<Tile> tiles)
    {
        var x = 150;

        var y = 1;

        foreach (var tile in tiles)
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

        for (var ty = 0; ty < 10; ty++)
        {
            Console.SetCursorPosition(x, y + ty);

            var line = $"{tile.Image[0, ty]}{tile.Image[1, ty]}{tile.Image[2, ty]}{tile.Image[3, ty]}{tile.Image[4, ty]}{tile.Image[5, ty]}{tile.Image[6, ty]}{tile.Image[7, ty]}{tile.Image[8, ty]}{tile.Image[9, ty]}";

            if (ty == 0 || ty == 9)
            {
                Console.WriteLine(line);

                continue;
            }

            Console.Write(line[0]);

            Console.ForegroundColor = ConsoleColor.DarkGray;

            Console.Write(line.Substring(1, 8));

            Console.ForegroundColor = ConsoleColor.Green;

            Console.Write(line[9]);
        }

        Console.ForegroundColor = ConsoleColor.Blue;

        Console.SetCursorPosition(x + 3, y + 4);

        Console.Write(tile.Id);

        Console.ForegroundColor = ConsoleColor.Green;
    }
}
#endif