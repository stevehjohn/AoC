﻿using AoC.Exceptions;
using AoC.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions._2019._02;

[UsedImplicitly]
public class Part2 : Solution
{
    public override string GetAnswer()
    {
        var cpu = new Computer.Cpu();

        cpu.Initialise();

        for (var noun = 0; noun < 100; noun++)
        {
            for (var verb = 0; verb < 100; verb++)
            {
                cpu.LoadProgram(Input);

                cpu.Memory[1] = noun;

                cpu.Memory[2] = verb;

                cpu.Run();

                if (cpu.Memory[0] == 19690720)
                {
                    return (100 * noun + verb).ToString();
                }
            }
        }

        throw new PuzzleException("Noun and verb combination not found.");
    }
}