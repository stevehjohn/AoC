﻿using System.Text;
using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;
using AoC.Solutions.Solutions._2019.Computer;

namespace AoC.Solutions.Solutions._2019._17;

public abstract class Base : Solution
{
    public override string Description => "Scaffolding vacuum robot";

    protected char[,] Map;

    protected void GetMap()
    {
        var cpu = new Cpu();

        cpu.Initialise(65536);

        cpu.LoadProgram(Input);

        cpu.Run();

        var output = new StringBuilder();

        while (cpu.UserOutput.Count > 0)
        {
            output.Append((char) cpu.UserOutput.Dequeue());
        }

        Map = output.ToString().Trim().To2DArray();
    }
}