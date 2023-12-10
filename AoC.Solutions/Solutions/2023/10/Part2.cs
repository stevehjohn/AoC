using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._10;

[UsedImplicitly]
public class Part2 : Base
{
    private int Width;

    private int Height;
    
    public override string GetAnswer()
    {
        ParseInput();
        
        return "Unknown";
    }

    private void ParseInput()
    {
        Height = Input.Length * 3 + 2;

        Width = Input[0].Length * 3 + 2;

        Map = new char[Height][];

        for (var y = 0; y < Height; y++)
        {
            Map[y] = new char[Width];
        }

        Array.Fill(Map[0], '.');
        
        Array.Fill(Map[Height - 1], '.');

        for (var y = 1; y < Height - 1; y += 3)
        {
            Array.Fill(Map[y], '.');
            Array.Fill(Map[y + 1], '.');
            Array.Fill(Map[y + 2], '.');

            for (var x = 1; x < Width - 1; x += 3)
            {
                ParseCell(x, y, Input[(y - 1) / 3][(x - 1) / 3]);
            }
        }

        foreach (var line in Map)
        {
            Console.WriteLine(new string(line));
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