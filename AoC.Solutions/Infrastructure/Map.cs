namespace AoC.Solutions.Infrastructure;

public class Map
{
    public int Width { get; private set; }
    
    public int Height { get; private set; }

    public char this[int x, int y] => _cells[y, x];
    
    private char[,] _cells;
    
    public Map(string[] input)
    {
        Initialise(input);
    }

    private void Initialise(string[] input)
    {
        Width = input[0].Length;

        Height = input.Length;

        _cells = new char[Height, Width];

        var y = 0;
        
        foreach (var line in input)
        {
            for (var x = 0; x < Width; x++)
            {
                _cells[y, x] = line[x];
            }

            y++;
        }
    }
}