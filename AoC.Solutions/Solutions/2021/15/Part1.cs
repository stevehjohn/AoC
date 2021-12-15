﻿using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._15;

[UsedImplicitly]
public class Part1 : Solution
{
    public override string Description => "Risky business";

    private int _width;

    private int _height;

    private int[,] _map;

    private int _minimumCost = int.MaxValue;

    public override string GetAnswer()
    {
        ParseInput();

        Solve(new Point(0, 0), new List<Point>());

        return _minimumCost.ToString();
    }

    private void ParseInput()
    {
        _width = Input[0].Length;

        _height = Input.Length;
        
        _map = new int[_width, _height];

        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                _map[x, y] = (byte) (Input[y][x] - '0');
            }
        }
    }
    
    // TODO: Exit if exceeding lowest cost so far.
    private void Solve(Point position, List<Point> visited, int cost = 0)
    {
        var neighbors = GetNeighbors(position);

        visited.Add(position);

        if (cost > _minimumCost)
        {
            return;
        }

        foreach (var neighbor in neighbors)
        {
            if (visited.Any(v => v.X == neighbor.X && v.Y == neighbor.Y))
            {
                continue;
            }

            if (neighbor.X == _width - 1 && neighbor.Y == _height - 1)
            {
                cost += _map[neighbor.X, neighbor.Y];

                if (cost < _minimumCost)
                {
                    _minimumCost = cost;
                }
            }

            Solve(neighbor, visited.ToList(), cost + _map[neighbor.X, neighbor.Y]);
        }
    }

    private List<Point> GetNeighbors(Point position)
    {
        var neighbors = new List<Point>();

        if (position.X < _width - 1)
        {
            neighbors.Add(new Point(position.X + 1, position.Y));
        }

        if (position.Y < _height - 1)
        {
            neighbors.Add(new Point(position.X, position.Y + 1));
        }

        if (position.X > 1)
        {
            neighbors.Add(new Point(position.X - 1, position.Y));
        }

        if (position.Y > 1)
        {
            neighbors.Add(new Point(position.X, position.Y - 1));
        }

        return neighbors;
    }
}