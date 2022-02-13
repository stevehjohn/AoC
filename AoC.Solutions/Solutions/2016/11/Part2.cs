﻿using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._11;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var state = ParseInput();

        DumpState(state);

        state[0] |= 32 | 64 | (32 << 16) | (64 << 16);
        
        DumpState(state);

        var result = Solve(state);

        return result.ToString();
    }
}