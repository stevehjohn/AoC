﻿namespace AoC.Solutions.Solutions._2022._15;

public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var dead = GetDeadZones(2_000_000);
        //var dead = GetDeadZones(10);

        return dead.ToString();
    }
}