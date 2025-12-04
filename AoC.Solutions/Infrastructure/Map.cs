namespace AoC.Solutions.Infrastructure;

public sealed class Map
{
    private readonly int _width;

    private readonly int _height;

    private readonly char[] _cells;

    public char this[int x, int y]
    {
        private get => _cells[y * _width + x];
        set => _cells[y * _width + x] = value;
    }

    public Map(string[] input)
    {
        _width = input[0].Length;

        _height = input.Length;

        var length = _width * _height;

        _cells = new char[length];

        var i = 0;

        foreach (var line in input)
        {
            for (var x = 0; x < _width; x++)
            {
                _cells[i++] = line[x];
            }
        }
    }

    public void ForAllCells(Action<int, int, char> action)
    {
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                action(x, y, this[x, y]);
            }
        }
    }

    public void ForAdjacentCells(int x, int y, Action<char> action)
    {
        var minY = Math.Max(0, y - 1);
        
        var maxY = Math.Min(_height - 1, y + 1);
        
        var minX = Math.Max(0, x - 1);
        
        var maxX = Math.Min(_width - 1, x + 1);

        for (var y1 = minY; y1 <= maxY; y1++)
        {
            var row = y1 * _width;

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
}