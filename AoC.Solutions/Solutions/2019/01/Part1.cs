﻿using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._01;

[UsedImplicitly]
public class Part1 : Solution
{
    public override string Description => "Calculate launch fuel";

    public override string GetAnswer()
    {
        var fuel = 0;

        foreach (var line in Input)
        {
            fuel += int.Parse(line) / 3 - 2;
        }

        return fuel.ToString();
    }
}