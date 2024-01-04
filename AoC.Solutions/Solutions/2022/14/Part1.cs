﻿using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2022._14;

[UsedImplicitly]
public class Part1 : Base
{
    public Part1()
    {
    }

    public Part1(IVisualiser<PuzzleState> visualiser) : base(visualiser)
    {
    }

    public override string GetAnswer()
    {
        CreateCave();

        var result = SimulateSand();

        return result.ToString();
    }
}