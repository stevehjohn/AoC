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
        for (var y1 = -1; y1 < 2; y1++)
        {
            for (var x1 = -1; x1 < 2; x1++)
            {
                if (x1 == 0 && y1 == 0)
                {
                    continue;
                }

                action(SafeGetCell(x + x1, y + y1));
            }
        }
    }

    private char SafeGetCell(int x, int y)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height)
        {
            return '\0';
        }
        
        return _cells[y * Width + x];
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