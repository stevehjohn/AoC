﻿using System.Security.Cryptography;
using System.Text;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2015._04;

public abstract class Base : Solution
{
    public override string Description => "Crypto stocking stuffer";

    protected int GetAnswer(string pattern)
    {
        var key = Input[0];

        var i = 1;

        while (true)
        {
            var hash = Convert.ToHexString(MD5.HashData(Encoding.ASCII.GetBytes($"{key}{i}")));

            if (hash.StartsWith(pattern))
            {
                break;
            }

            i++;
        }

        return i;
    }
}