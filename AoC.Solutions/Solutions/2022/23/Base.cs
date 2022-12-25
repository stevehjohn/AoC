using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._23;

public abstract class Base : Solution
{
    public override string Description => "Unstable diffusion";

    private const int SetMaxSize = 2_500;

    private HashSet<Point> _elves = new(SetMaxSize);

    private readonly List<Func<Point, Point>> _evaluations = new();

    private readonly HashSet<Point> _visited = new(SetMaxSize);

    private readonly HashSet<Point> _blocked = new(SetMaxSize);

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
        _visited.Clear();

        _blocked.Clear();

        foreach (var elf in _elves)
        {
            var proposedMove = GetProposedMove(elf);

            if (proposedMove == null)
            {
                continue;
            }

            if (_visited.Contains(proposedMove))
            {
                _blocked.Add(proposedMove);

                continue;
            }

            _visited.Add(proposedMove);
        }

        var moved = 0;

        var elves = new HashSet<Point>(SetMaxSize);

        foreach (var elf in _elves)
        {
            var proposedMove = GetProposedMove(elf);

            if (proposedMove == null || _blocked.Contains(proposedMove))
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