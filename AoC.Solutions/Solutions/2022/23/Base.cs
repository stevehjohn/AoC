﻿using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._23;

public abstract class Base : Solution
{
    public override string Description => "Unstable diffusion";

    private HashSet<Point> _elves = new();

    private readonly List<Func<Point, Point>> _evaluations = new();

    protected void ParseInput()
    {
        for (var y = 0; y < Input.Length; y++)
        {
            var line = Input[y];

            for (var x = 0; x < line.Length; x++)
            {
                if (line[x] == '#')
                {
                    _elves.Add(new Point(x, y));
                }
            }
        }
    }

    protected int RunSimulation(bool toCompletion = false)
    {
        InitialiseEvaluations();

        var i = toCompletion ? int.MaxValue : 10;

        var rounds = 1;

        while (i > 0)
        {
            if (RunSimulationStep() == 0)
            {
                break;
            }

            i--;

            rounds++;
        }

        if (toCompletion)
        {
            return rounds;
        }

        return CountEmptyTiles();
    }

    protected int RunSimulationStep()
    {
        var moves = new Dictionary<Point, int>();

        foreach (var elf in _elves)
        {
            var proposedMove = GetProposedMove(elf);

            if (proposedMove == null)
            {
                continue;
            }

            if (moves.ContainsKey(proposedMove))
            {
                moves[proposedMove]++;

                continue;
            }

            moves.Add(proposedMove, 1);
        }

        var elves = new HashSet<Point>();

        var moved = 0;

        foreach (var elf in _elves)
        {
            var proposedMove = GetProposedMove(elf);

            if (proposedMove == null || moves[proposedMove] > 1)
            {
                elves.Add(elf);

                continue;
            }

            elves.Add(proposedMove);

            moved++;
        }

        _elves = elves;

        RotateEvaluations();

        return moved;
    }

    private void InitialiseEvaluations()
    {
        _evaluations.Add(p => ! _elves.Contains(new Point(p.X - 1, p.Y - 1))
                              && ! _elves.Contains(new Point(p.X, p.Y - 1))
                              && ! _elves.Contains(new Point(p.X + 1, p.Y - 1))
                                  ? new Point(p.X, p.Y - 1)
                                  : null);

        _evaluations.Add(p => ! _elves.Contains(new Point(p.X - 1, p.Y + 1))
                              && ! _elves.Contains(new Point(p.X, p.Y + 1))
                              && ! _elves.Contains(new Point(p.X + 1, p.Y + 1))
                                  ? new Point(p.X, p.Y + 1)
                                  : null);

        _evaluations.Add(p => ! _elves.Contains(new Point(p.X - 1, p.Y - 1))
                              && ! _elves.Contains(new Point(p.X - 1, p.Y))
                              && ! _elves.Contains(new Point(p.X - 1, p.Y + 1))
                                  ? new Point(p.X - 1, p.Y)
                                  : null);

        _evaluations.Add(p => ! _elves.Contains(new Point(p.X + 1, p.Y - 1))
                              && ! _elves.Contains(new Point(p.X + 1, p.Y))
                              && ! _elves.Contains(new Point(p.X + 1, p.Y + 1))
                                  ? new Point(p.X + 1, p.Y)
                                  : null);
    }

    private Point GetProposedMove(Point position)
    {
        if (! _elves.Contains(new Point(position.X - 1, position.Y - 1))
            && ! _elves.Contains(new Point(position.X, position.Y - 1))
            && ! _elves.Contains(new Point(position.X + 1, position.Y - 1))
            && ! _elves.Contains(new Point(position.X + 1, position.Y))
            && ! _elves.Contains(new Point(position.X + 1, position.Y + 1))
            && ! _elves.Contains(new Point(position.X, position.Y + 1))
            && ! _elves.Contains(new Point(position.X - 1, position.Y + 1))
            && ! _elves.Contains(new Point(position.X - 1, position.Y)))
        {
            return null;
        }

        Point newPosition = null;

        foreach (var evaluation in _evaluations)
        {
            newPosition = evaluation(position);

            if (newPosition != null)
            {
                break;
            }
        }

        return newPosition;
    }

    private void RotateEvaluations()
    {
        var first = _evaluations[0];

        _evaluations.RemoveAt(0);

        _evaluations.Add(first);
    }

    private int CountEmptyTiles()
    {
        var minX = _elves.Min(e => e.X);
        var maxX = _elves.Max(e => e.X);

        var minY = _elves.Min(e => e.Y);
        var maxY = _elves.Max(e => e.Y);

        var empty = 0;

        for (var y = minY; y <= maxY; y++)
        {
            for (var x = minX; x <= maxX; x++)
            {
                if (_elves.Contains(new Point(x, y)))
                {
                    continue;
                }

                empty++;
            }
        }

        return empty;
    }
}