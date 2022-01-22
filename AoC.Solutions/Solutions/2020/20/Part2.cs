using AoC.Solutions.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._20;

[UsedImplicitly]
public class Part2 : Base
{
    private char[,] _image;

    private int _width;

    private int _height;

    private readonly List<Point> _monster = new();

    public override string GetAnswer()
    {
        var input = File.ReadAllLines(Part1ResultFile);

        ParseInput(input);

        ParseMonster();

#if DEBUG && DUMP
        Visualiser.DumpImage(_image);
#endif

        var monsters = FindMonsters();

        return "TESTING";
    }

    private List<Point> FindMonsters()
    {
        var result = new List<Point>();

        for (var y = 0; y < _height - 2; y++)
        {
            for (var x = 0; x < _width - 19; x++)
            {
                if (IsMonsterAt(x, y))
                {
                    result.Add(new Point(x, y));
                }
            }
        }

        return result;
    }

    private bool IsMonsterAt(int x, int y)
    {
        foreach (var point in _monster)
        {
            if (_image[x + point.X, y + point.Y] != '#')
            {
                return false;
            }
        }

        return true;
    }

    private void ParseMonster()
    {
        var monster = "                  # \n#    ##    ##    ###\n #  #  #  #  #  #".Split('\n');

        for (var y = 0; y < monster.Length; y++)
        {
            for (var x = 0; x < monster[y].Length; x++)
            {
                if (monster[y][x] == '#')
                {
                    _monster.Add(new Point(x, y));
                }
            }
        }
    }

    private void ParseInput(string[] input)
    {
        var y = 0;

        _width = input[0].Length;

        _height = input.Length;

        _image = new char[_width, _height];

        foreach (var line in input)
        {
            for (var x = 0; x < _width; x++)
            {
                _image[x, y] = line[x];
            }

            y++;
        }
    }
}