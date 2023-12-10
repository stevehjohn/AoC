using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._10;

[UsedImplicitly]
public class Part2 : Base
{
    private int _width;

    private int _height;
    
    public override string GetAnswer()
    {
        ParseInput();

        FloodFill(0, 0);

        return "Unknown";
    }

    private void FloodFill(int x, int y)
    {
        Map[y][x] = 'X';

        if (x > 0 && Map[y][x - 1] == '.')
        {
            FloodFill(x - 1, y);
        }

        if (x < _width - 1 && Map[y][x + 1] == '.')
        {
            FloodFill(x + 1, y);
        }

        if (y < _height - 1 && Map[y + 1][x] == '.')
        {
            FloodFill(x, y + 1);
        }

        if (y > 0 && Map[y - 1][x] == '.')
        {
            FloodFill(x, y - 1);
        }
    }

    private void ParseInput()
    {
        _height = Input.Length * 3 + 2;

        _width = Input[0].Length * 3 + 2;

        Map = new char[_height][];

        for (var y = 0; y < _height; y++)
        {
            Map[y] = new char[_width];
        }

        Array.Fill(Map[0], '.');
        
        Array.Fill(Map[_height - 1], '.');

        for (var y = 1; y < _height - 1; y += 3)
        {
            Array.Fill(Map[y], '.');
            Array.Fill(Map[y + 1], '.');
            Array.Fill(Map[y + 2], '.');

            for (var x = 1; x < _width - 1; x += 3)
            {
                ParseCell(x, y, Input[(y - 1) / 3][(x - 1) / 3]);
            }
        }
    }

    private void ParseCell(int x, int y, char cell)
    {
        if (cell != '.')
        {
            Map[y + 1][x + 1] = '#';
        }

        switch (cell)
        {
            case '-':
                Map[y + 1][x] = '#';
                Map[y + 1][x + 2] = '#';
                break;

            case '|':
                Map[y][x + 1] = '#';
                Map[y + 2][x + 1] = '#';
                break;
            
            case 'S':
                Map[y + 1][x] = '#';
                Map[y + 1][x + 2] = '#';
                Map[y][x + 1] = '#';
                Map[y + 2][x + 1] = '#';
                break;
            
            case '7':
                Map[y + 1][x] = '#';
                Map[y + 2][x + 1] = '#';
                break;
            
            case 'J':
                Map[y + 1][x] = '#';
                Map[y][x + 1] = '#';
                break;
            
            case 'F':
                Map[y + 1][x + 2] = '#';
                Map[y + 2][x + 1] = '#';
                break;
            
            case 'L':
                Map[y][x + 1] = '#';
                Map[y + 1][x + 2] = '#';
                break;
        }
    }
}