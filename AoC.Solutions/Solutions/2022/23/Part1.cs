﻿namespace AoC.Solutions.Solutions._2022._23;

public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var result = RunSimulation();

        return result.ToString();
    }
}