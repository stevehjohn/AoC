namespace AoC.Solutions.Infrastructure;

public class Map
{
    private int Width { get; set; }

    private int Height { get; set; }

    private int _length;

    private char[] _cells;

    public char this[int x, int y]
    {
        private get => _cells[y * Width + x];
        set => _cells[y * Width + x] = value;
    }

    public Map(string[] input)
    {
        Initialise(input);
    }

    public void ForAllCells(Action<int, int, char> action)
    {
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                action(x, y, this[x, y]);
            }
        }
    }

    public void ForAdjacentCells(int x, int y, Action<char> action)
    {
        var minY = Math.Max(0, y - 1);
        
        var maxY = Math.Min(Height - 1, y + 1);
        
        var minX = Math.Max(0, x - 1);
        
        var maxX = Math.Min(Width - 1, x + 1);

        for (var y1 = minY; y1 <= maxY; y1++)
        {
            var row = y1 * Width;

            for (var x1 = minX; x1 <= maxX; x1++)
            {
                if (x1 == x && y1 == y)
                {
                    continue;
                }

                action(_cells[row + x1]);
            }
        }
    }

    private void Initialise(string[] input)
    {
        Width = input[0].Length;

        Height = input.Length;

        _length = Width * Height;

        _cells = new char[_length];

        var i = 0;

        foreach (var line in input)
        {
            for (var x = 0; x < Width; x++)
            {
                _cells[i++] = line[x];
            }
        }
    }
}