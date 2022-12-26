﻿using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._23;

public abstract class Base : Solution
{
    public override string Description => "Unstable diffusion";

    private const int SetMaxSize = 2_500;

    private HashSet<Point> _elves = new(SetMaxSize);

    private readonly Func<Point, Point>[] _evaluations = new Func<Point, Point>[4];

    private int _startEvaluation;

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
        var elves = new HashSet<Point>(SetMaxSize);

        var moved = 0;

        foreach (var elf in _elves)
        {
            var proposedMove = GetProposedMove(elf);

            if (proposedMove == null)
            {
                elves.Add(elf);

                continue;
            }

            if (elves.Contains(elf + proposedMove))
            {
                elves.Add(elf);

                elves.Remove(elf + proposedMove);

                elves.Add(elf + proposedMove + proposedMove);

                moved--;

                continue;
            }

            elves.Add(elf + proposedMove);

            moved++;
        }

        _elves = elves;

        RotateEvaluations();

        return moved;
    }
    
    private void Dump()
    {
        var minX = _elves.Min(e => e.X);
        var maxX = _elves.Max(e => e.X);

        var minY = _elves.Min(e => e.Y);
        var maxY = _elves.Max(e => e.Y);

        for (var y = minY; y <= maxY; y++)
        {
            for (var x = minX; x <= maxX; x++)
            {
                if (_elves.Contains(new Point(x, y)))
                {
                    Console.Write('#');

                    continue;
                }

                Console.Write('.');
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }

    private void InitialiseEvaluations()
    {
        _evaluations[0] = p => ! (_elves.Contains(new Point(p.X - 1, p.Y - 1))
                                  || _elves.Contains(new Point(p.X, p.Y - 1))
                                  || _elves.Contains(new Point(p.X + 1, p.Y - 1)))
                                   ? new Point(0, - 1)
                                   : null;

        _evaluations[1] = p => ! (_elves.Contains(new Point(p.X - 1, p.Y + 1))
                                  || _elves.Contains(new Point(p.X, p.Y + 1))
                                  || _elves.Contains(new Point(p.X + 1, p.Y + 1)))
                                   ? new Point(0, 1)
                                   : null;

        _evaluations[2] = p => ! (_elves.Contains(new Point(p.X - 1, p.Y - 1))
                                  || _elves.Contains(new Point(p.X - 1, p.Y))
                                  || _elves.Contains(new Point(p.X - 1, p.Y + 1)))
                                   ? new Point(- 1, 0)
                                   : null;

        _evaluations[3] = p => ! (_elves.Contains(new Point(p.X + 1, p.Y - 1))
                                  || _elves.Contains(new Point(p.X + 1, p.Y))
                                  || _elves.Contains(new Point(p.X + 1, p.Y + 1)))
                                   ? new Point(1, 0)
                                   : null;
    }

    private Point GetProposedMove(Point position)
    {
        if (! (_elves.Contains(new Point(position.X - 1, position.Y - 1))
               || _elves.Contains(new Point(position.X, position.Y - 1))
               || _elves.Contains(new Point(position.X + 1, position.Y - 1))
               || _elves.Contains(new Point(position.X + 1, position.Y))
               || _elves.Contains(new Point(position.X + 1, position.Y + 1))
               || _elves.Contains(new Point(position.X, position.Y + 1))
               || _elves.Contains(new Point(position.X - 1, position.Y + 1))
               || _elves.Contains(new Point(position.X - 1, position.Y))))
        {
            return null;
        }

        var evaluationIndex = _startEvaluation;

        for (var i = 0; i < 4; i++)
        {
            var newPosition = _evaluations[evaluationIndex](position);

            if (newPosition != null)
            {
                return newPosition;
            }

            evaluationIndex++;

            if (evaluationIndex == 4)
            {
                evaluationIndex = 0;
            }
        }

        return null;
    }

    private void RotateEvaluations()
    {
        _startEvaluation++;

        if (_startEvaluation == 4)
        {
            _startEvaluation = 0;
        }
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