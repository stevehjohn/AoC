﻿using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2018._20;

public abstract class Base : Solution
{
    public override string Description => "Regex maze";

    private readonly List<(Point Position, int Distance)> _maze = new();

    private (Point Position, int Distance) _position;

    protected void Solve()
    {
        var data = Input[0][1..^2];

        _position = (new Point(0, 0), 0);

        _maze.Add(_position);

        var stack = new Stack<(Point Position, int Distance)>();

        foreach (var c in data)
        {
            switch (c)
            {
                case 'N':
                    AddRoom(0, -1);

                    break;
                case 'E':
                    AddRoom(1, 0);

                    break;
                case 'S':
                    AddRoom(0, 1);

                    break;
                case 'W':
                    AddRoom(-1, 0);

                    break;
                case '(':
                    stack.Push(_position);

                    break;
                case ')':
                    _position = stack.Pop();

                    break;
                case '|':
                    _position = stack.ToList()[0];

                    break;
            }
        }

        var furthest = _maze.MaxBy(p => p.Distance);

        // Now find shortest path to furthest.
    }

    private void AddRoom(int dX, int dY)
    {
        _position = (new Point(_position.Position.X + dX, _position.Position.Y + dY), _position.Distance + 1);

        _maze.Add(_position);
    }
}