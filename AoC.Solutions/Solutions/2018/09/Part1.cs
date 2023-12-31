﻿using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._09;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var parameters = ParseInput();

        var result = Play(parameters.Players, parameters.LastMarbleValue);

        return result.ToString();
    }
}