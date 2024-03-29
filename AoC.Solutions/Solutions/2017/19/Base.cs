﻿using System.Text;
using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2017._19;

public abstract class Base : Solution
{
    public override string Description => "Packet map";

    private char[,] _map;

    protected (string Letters, int Steps) TravelAndCollect(int startX)
    {
        var result = new StringBuilder();

        var position = new Point(startX, 0);

        var direction = new Point(0, 1);

        var steps = 0;

        while (_map[position.X, position.Y] != ' ')
        {
            steps++;

            position.X += direction.X;

            position.Y += direction.Y;

            switch (_map[position.X, position.Y])
            {
                case >= 'A' and <= 'Z':
                    result.Append(_map[position.X, position.Y]);

                    continue;
                case '+':
                {
                    var newDirection = new Point(-direction.Y, direction.X);

                    direction = _map[position.X + newDirection.X, position.Y + newDirection.Y] != ' ' ? newDirection : new Point(direction.Y, -direction.X);

                    break;
                }
            }
        }

        return (result.ToString(), steps);
    }

    protected int ParseInputAndReturnStartX()
    {
        _map = new char[Input[0].Length, Input.Length];

        var y = 0;

        var startX = 0;

        foreach (var line in Input)
        {
            var x = 0;

            foreach (var c in line)
            {
                if (y == 0 && c != ' ')
                {
                    startX = x;
                }

                _map[x, y] = c;

                x++;
            }

            y++;
        }

        return startX;
    }
}