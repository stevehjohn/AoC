﻿using AoC.Solutions.Infrastructure;
using AoC.Solutions.Solutions._2019.Computer;

namespace AoC.Solutions.Solutions._2019._09;

public abstract class Base : Solution
{
    public string GetAnswer(int input)
    {
        var cpu = new Cpu();

        cpu.Initialise(65536);

        cpu.LoadProgram(Input);

        cpu.UserInput.Enqueue(input);

        cpu.Run();

        return cpu.UserOutput.Dequeue().ToString();
    }
}