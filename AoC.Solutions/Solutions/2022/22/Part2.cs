﻿namespace AoC.Solutions.Solutions._2022._22;

public class Part2 : Base
{
    public override string GetAnswer()
    {
        var cube = new Cube();
            
        cube.BuildFromInput(Input[..^2]);

        return "";
    }
}