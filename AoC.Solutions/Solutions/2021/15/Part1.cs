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

    private bool[,] _visited;

    public override string GetAnswer()
    {
        ParseInput();

        Solve(0, 0);

        return _minimumCost.ToString();
    }

    private void ParseInput()
    {
        _width = Input[0].Length;

        _height = Input.Length;
        
        _map = new int[_width, _height];
        
        _visited = new bool[_width, _height];

        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                _map[x, y] = (byte) (Input[y][x] - '0');
            }
        }
    }
    
    private void Solve(int x, int y, int cost = 0)
    {
        if (cost > _minimumCost)
        {
            return;
        }

        _visited[x, y] = true;

        if (x < _width - 1)
        {
            CheckNeighbor(x + 1, y, cost);
        }

        if (x > 0)
        {
            CheckNeighbor(x - 1, y, cost);
        }

        if (y < _height - 1)
        {
            CheckNeighbor(x, y + 1, cost);
        }

        if (y > 0)
        {
            CheckNeighbor(x, y - 1, cost);
        }
    }

    private void CheckNeighbor(int x, int y, int cost)
    {
        if (_visited[x, y])
        {
            return;
        }

        if (x == _width - 1 && y == _height - 1)
        {
            cost += _map[x, y];

            if (cost < _minimumCost)
            {
                _minimumCost = cost;
            }

            return;
        }

        Solve(x, y, cost + _map[x, y]);

        _visited[x, y] = false;
    }
}