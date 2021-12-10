﻿using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._03;

[UsedImplicitly]
public class Part1 : Solution
{
    public override string GetAnswer()
    {
        var length = Input[0].Length;

        var bit = (int) Math.Pow(2, length - 1);

        var value = 0;

        var mask = 0;

        for (var i = 0; i < length; i++)
        {
            var ones = 0;

            foreach (var line in Input)
            {
                if (line[i] == '1')
                {
                    ones++;
                }
            }

            if (ones > Input.Length / 2)
            {
                value += bit;
            }

            mask += bit;

            bit >>= 1;
        }

        return (value * (value ^ mask)).ToString();
    }
}