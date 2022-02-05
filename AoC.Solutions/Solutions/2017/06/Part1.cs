﻿using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._06;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var result = RunUntilRepeats();

        return result.ToString();
    }
}