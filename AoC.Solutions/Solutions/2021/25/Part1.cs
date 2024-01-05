using AoC.Solutions.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._25;

[UsedImplicitly]
public class Part1 : Base
{
    private int _width;

    private int _height;

    private CellState[,] _cells;

    private readonly HashSet<(Point Source, Point Target)> _cellsToMove = [];

    public override string GetAnswer()
    {
        ParseInput();

        var steps = CountStepsToNoMovement();

        return steps.ToString();
    }

    private int CountStepsToNoMovement()
    {
        var moved = true;

        var steps = 0;

        while (moved)
        {
            moved = false;

            steps++;

            foreach (var state in new[] { CellState.East, CellState.South })
            {
                _cellsToMove.Clear();

                for (var y = 0; y < _height; y++)
                {
                    for (var x = 0; x < _width; x++)
                    {
                        switch (state)
                        {
                            case CellState.East:
                                moved |= CanMoveCell(x, y, 1, 0, CellState.East);
                                break;
                            case CellState.South:
                                moved |= CanMoveCell(x, y, 0, 1, CellState.South);
                                break;
                        }
                    }
                }

                MoveCells();
            }

#if DEBUG && DUMP
            Dump();
#endif
        }

        return steps;
    }

#if DEBUG && DUMP
    private void Dump()
    {
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                Console.Write(_cells[x, y] == CellState.East
                                  ? '>'
                                  : _cells[x, y] == CellState.South
                                      ? 'v'
                                      : '.');
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }
#endif

    private void MoveCells()
    {
        foreach (var move in _cellsToMove)
        {
            _cells[move.Target.X, move.Target.Y] = _cells[move.Source.X, move.Source.Y];

            _cells[move.Source.X, move.Source.Y] = CellState.Empty;
        }
    }

    private bool CanMoveCell(int x, int y, int dX, int dY, CellState state)
    {
        if (_cells[x, y] != state)
        {
            return false;
        }

        var checkX = x + dX;

        if (checkX >= _width)
        {
            checkX = 0;
        }

        var checkY = y + dY;

        if (checkY >= _height)
        {
            checkY = 0;
        }

        if (_cells[checkX, checkY] == CellState.Empty)
        {
            _cellsToMove.Add((Source: new Point(x, y), Target: new Point(checkX, checkY)));

            return true;
        }

        return false;
    }

    private void ParseInput()
    {
        _width = Input[0].Length;

        _height = Input.Length;

        _cells = new CellState[_width, _height];

        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                _cells[x, y] = Input[y][x] == '>'
                    ? CellState.East
                    : Input[y][x] == 'v'
                        ? CellState.South
                        : CellState.Empty;
            }
        }
    }
}