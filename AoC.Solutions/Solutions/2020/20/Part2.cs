﻿using AoC.Solutions.Common;
using AoC.Solutions.Exceptions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._20;

[UsedImplicitly]
public class Part2 : Base
{
    private char[,] _image;

    private int _width;

    private int _height;

    private readonly List<Point> _monster = [];

    public override string GetAnswer()
    {
        var input = File.ReadAllLines(Part1ResultFile);

        ParseInput(input);

        ParseMonster();

        List<Point> monsters = null;

        for (var r = 0; r < 4; r++)
        {
            monsters = FindMonsters();

            if (monsters.Count > 0)
            {
                break;
            }

            FlipHorizontal();

            monsters = FindMonsters();

            if (monsters.Count > 0)
            {
                break;
            }

            FlipHorizontal();

            FlipVertical();

            monsters = FindMonsters();

            if (monsters.Count > 0)
            {
                break;
            }

            FlipVertical();

            Rotate();

            monsters = FindMonsters();

            if (monsters.Count > 0)
            {
                break;
            }
        }

        if (monsters == null || monsters.Count == 0)
        {
            throw new PuzzleException("Solution not found.");
        }

        return GetWaterRoughness().ToString();
    }

    private int GetWaterRoughness()
    {
        var result = 0;

        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                if (_image[x, y] == '#')
                {
                    result++;
                }
            }
        }

        return result;
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

                    foreach (var point in _monster)
                    {
                        _image[x + point.X, y + point.Y] = 'O';
                    }
                }
            }
        }

        return result;
    }

    private void Rotate()
    {
        var image = new char[_width, _height];

        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                image[x, y] = _image[y, _width - x - 1];
            }
        }

        _image = image;
    }

    private void FlipHorizontal()
    {
        var image = new char[_width, _height];

        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                image[x, y] = _image[_height - x - 1, y];
            }
        }

        _image = image;
    }

    private void FlipVertical()
    {
        var image = new char[_width, _height];

        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                image[x, y] = _image[x, _width - y - 1];
            }
        }

        _image = image;
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