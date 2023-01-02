﻿namespace AoC.Solutions.Solutions._2022._23;

public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var result = RunSimulation();

        return result.ToString();
    }

    private const int YOffset = 125;

    private readonly HashSet<int> _elves = new(SetMaxSize);

    private readonly Func<int, int>[] _evaluations = new Func<int, int>[4];
    
    private void ParseInput()
    {
        for (var y = 0; y < Input.Length; y++)
        {
            var line = Input[y];

            for (var x = 0; x < line.Length; x++)
            {
                if (line[x] == '#')
                {
                    _elves.Add(x + y * YOffset);
                }
            }
        }
    }

    private int RunSimulation()
    {
        InitialiseEvaluations();

        var rounds = 1;

        while (true)
        {
            if (RunSimulationStep() == 0)
            {
                break;
            }

            rounds++;
        }

        return rounds;
    }

    private int RunSimulationStep()
    {
        var moved = 0;

        var moves = new Dictionary<int, int>();

        foreach (var elf in _elves)
        {
            var proposedMove = GetProposedMove(elf);

            if (proposedMove == 0)
            {
                continue;
            }

            if (moves.ContainsKey(elf + proposedMove))
            {
                moves.Remove(elf + proposedMove);

                moved--;

                continue;
            }

            moves.Add(elf + proposedMove, elf);

            moved++;
        }

        foreach (var move in moves)
        {
            _elves.Remove(move.Value);

            _elves.Add(move.Key);
        }

        RotateEvaluations();

        return moved;
    }

    private void InitialiseEvaluations()
    {
        _evaluations[0] = p => ! (_elves.Contains(p - 1 - YOffset)
                                  || _elves.Contains(p - YOffset)
                                  || _elves.Contains(p + 1 - YOffset))
                                   ? -YOffset
                                   : 0;

        _evaluations[1] = p => ! (_elves.Contains(p - 1 + YOffset)
                                  || _elves.Contains(p + YOffset)
                                  || _elves.Contains(p + 1 + YOffset))
                                   ? YOffset
                                   : 0;

        _evaluations[2] = p => ! (_elves.Contains(p - 1 - YOffset)
                                  || _elves.Contains(p - 1)
                                  || _elves.Contains(p - 1 + YOffset))
                                   ? -1
                                   : 0;

        _evaluations[3] = p => ! (_elves.Contains(p + 1 - YOffset)
                                  || _elves.Contains(p + 1)
                                  || _elves.Contains(p + 1 + YOffset))
                                   ? 1
                                   : 0;
    }

    private int GetProposedMove(int position)
    {
        if (! (_elves.Contains(position - 1 - YOffset)
               || _elves.Contains(position - YOffset)
               || _elves.Contains(position + 1 - YOffset)
               || _elves.Contains(position + 1)
               || _elves.Contains(position + 1 + YOffset)
               || _elves.Contains(position + YOffset)
               || _elves.Contains(position - 1 + YOffset)
               || _elves.Contains(position - 1)))
        {
            return 0;
        }

        var evaluationIndex = StartEvaluation;

        for (var i = 0; i < 4; i++)
        {
            var newPosition = _evaluations[evaluationIndex](position);

            if (newPosition != 0)
            {
                return newPosition;
            }

            evaluationIndex++;

            if (evaluationIndex == 4)
            {
                evaluationIndex = 0;
            }
        }

        return 0;
    }
}