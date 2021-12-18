﻿using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._18;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var left = Number.Parse(Input[0]);

        for (var i = 1; i < Input.Length; i++)
        {
            var right = Number.Parse(Input[i]);

            left = Add(left, right);
        }

        return "TESTING";
    }

    private static Number Add(Number left, Number right)
    {
        return new Number
               {
                   Left = left,
                   Right = right
               };
    }
}