﻿using AoC.Solutions.Exceptions;

namespace AoC.Solutions.Solutions._2022._23;

public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var result = RunSimulation();

        return result.ToString();
    }

    private const int YOffset = 125;

    private const int NegativeOffset = 2_000;

    private const int ArbitraryArenaSize = 20_000;

    private readonly bool[] _cells = new bool[ArbitraryArenaSize];

    private void ParseInput()
    {
        for (var y = 0; y < Input.Length; y++)
        {
            var line = Input[y];

            for (var x = 0; x < line.Length; x++)
            {
                if (line[x] == '#')
                {
                    _cells[NegativeOffset + x + y * YOffset] = true;
                }
            }
        }
    }

    private int RunSimulation()
    {
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

        //foreach (var elf in _elves)
        for (var elf = 0; elf < 20_000; elf++)
        {
            if (! _cells[elf])
            {
                continue;
            }

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
            _cells[move.Value] = false;

            _cells[move.Key] = true;
        }

        RotateEvaluations();

        return moved;
    }

    private int Evaluate(int index, int position)
    {
        switch (index)
        {
            case 0:
                return ! (_cells[position - 1 - YOffset]
                          || _cells[position - YOffset]
                          || _cells[position + 1 - YOffset])
                           ? -YOffset
                           : 0;

            case 1:
                return ! (_cells[position - 1 + YOffset]
                          || _cells[position + YOffset]
                          || _cells[position + 1 + YOffset])
                           ? YOffset
                           : 0;

            case 2:
                return ! (_cells[position - 1 - YOffset]
                          || _cells[position - 1]
                          || _cells[position - 1 + YOffset])
                           ? -1
                           : 0;

            case 3:
                return ! (_cells[position + 1 - YOffset]
                          || _cells[position + 1]
                          || _cells[position + 1 + YOffset])
                           ? 1
                           : 0;
        }

        throw new PuzzleException("Evaluation index is not valid.");
    }

    private int GetProposedMove(int position)
    {
        if (! (_cells[position - 1 - YOffset]
               || _cells[position - YOffset]
               || _cells[position + 1 - YOffset]
               || _cells[position + 1]
               || _cells[position + 1 + YOffset]
               || _cells[position + YOffset]
               || _cells[position - 1 + YOffset]
               || _cells[position - 1]))
        {
            return 0;
        }

        var evaluationIndex = StartEvaluation;

        for (var i = 0; i < 4; i++)
        {
            var newPosition = Evaluate(evaluationIndex, position);

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