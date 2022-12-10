﻿using System.Text;
using AoC.Solutions.Common.Ocr;

namespace AoC.Solutions.Solutions._2022._10;

public class Part2 : Base
{
    //public override Variant? OcrOutput => Variant.Small;

    public override string GetAnswer()
    {
        var registerX = 1;

        var column = 0;

        var answer = new StringBuilder();

        foreach (var line in Input)
        {
            switch (line[..4])
            {
                case "noop":
                    column++;

                    if (column == 40)
                    {
                        column = 0;

                        Console.WriteLine();
                        answer.Append('\0');
                    }

                    Console.Write(Math.Abs(registerX - column) < 2 ? '#' : '.');
                    answer.Append(Math.Abs(registerX - column) < 2 ? '*' : '.');

                    break;

                case "addx":
                    column++;

                    if (column == 40)
                    {
                        column = 0;

                        Console.WriteLine();
                        answer.Append('\0');
                    }

                    Console.Write(Math.Abs(registerX - column) < 2 ? '#' : '.');
                    answer.Append(Math.Abs(registerX - column) < 2 ? '*' : '.');

                    column++;

                    if (column == 40)
                    {
                        column = 0;

                        Console.WriteLine();
                        answer.Append('\0');
                    }

                    registerX += int.Parse(line[5..]);

                    Console.Write(Math.Abs(registerX - column) < 2 ? '#' : '.');
                    answer.Append(Math.Abs(registerX - column) < 2 ? '*' : '.');

                    break;
            }
        }

        return answer.ToString();
    }
}