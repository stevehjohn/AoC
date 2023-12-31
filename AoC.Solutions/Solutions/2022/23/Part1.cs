using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2022._23;

public class Part1 : Base
{
    private HashSet<Point> _elves = new(SetMaxSize);

    private readonly Func<Point, Point>[] _evaluations = new Func<Point, Point>[4];

    private readonly Point[] _directions = { new(0, -1), new(1, 0), new(0, 1), new(-1, 0) };

    public override string GetAnswer()
    {
        ParseInput();

        var result = RunSimulation();

        return result.ToString();
    }

    private void ParseInput()
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

    private int RunSimulation(bool toCompletion = false)
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

    private int RunSimulationStep()
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

            if (! elves.Add(elf + proposedMove))
            {
                elves.Add(elf);

                elves.Remove(elf + proposedMove);

                elves.Add(elf + proposedMove + proposedMove);

                moved--;

                continue;
            }

            moved++;
        }

        _elves = elves;

        RotateEvaluations();

        return moved;
    }

    private void InitialiseEvaluations()
    {
        _evaluations[0] = p => ! (_elves.Contains(new Point(p.X - 1, p.Y - 1))
                                  || _elves.Contains(new Point(p.X, p.Y - 1))
                                  || _elves.Contains(new Point(p.X + 1, p.Y - 1)))
                                   ? _directions[0]
                                   : null;

        _evaluations[1] = p => ! (_elves.Contains(new Point(p.X - 1, p.Y + 1))
                                  || _elves.Contains(new Point(p.X, p.Y + 1))
                                  || _elves.Contains(new Point(p.X + 1, p.Y + 1)))
                                   ? _directions[2]
                                   : null;

        _evaluations[2] = p => ! (_elves.Contains(new Point(p.X - 1, p.Y - 1))
                                  || _elves.Contains(new Point(p.X - 1, p.Y))
                                  || _elves.Contains(new Point(p.X - 1, p.Y + 1)))
                                   ? _directions[3]
                                   : null;

        _evaluations[3] = p => ! (_elves.Contains(new Point(p.X + 1, p.Y - 1))
                                  || _elves.Contains(new Point(p.X + 1, p.Y))
                                  || _elves.Contains(new Point(p.X + 1, p.Y + 1)))
                                   ? _directions[1]
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

        var evaluationIndex = StartEvaluation;

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