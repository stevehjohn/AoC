﻿using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._07;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var wires = ParseInput();

        return "TESTING";
    }

    private Dictionary<string, (string LeftWire, int LeftValue, string Operation, string RightWire, int RightValue, string OutputWire)> ParseInput()
    {
        var result = new Dictionary<string, (string LeftWire, int LeftValue, string Operation, string RightWire, int RightValue, string OutputWire)>();

        foreach (var line in Input)
        {
            var data = ParseLine(line);

            result.Add(data.OutputWire, data);
        }

        return result;
    }

    private (string LeftWire, int LeftValue, string Operation, string RightWire, int RightValue, string OutputWire) ParseLine(string line)
    {
        var parts = line.Split(' ', StringSplitOptions.TrimEntries);

        var outputWire = parts[^1];

        if (parts.Length == 3)
        {
            return (null, int.Parse(parts[0]), null, null, 0, outputWire);
        }

        if (parts.Length == 4)
        {
            if (int.TryParse(parts[1], out var value))
            {
                return (null, 0, parts[0], null, value, outputWire);
            }

            return (null, 0, parts[0], parts[1], 0, outputWire);
        }

        var operation = parts[1];

        string leftWire = null;

        string rightWire = null;

        if (! int.TryParse(parts[0], out var leftValue))
        {
            leftWire = parts[0];
        }

        if (! int.TryParse(parts[2], out var rightValue))
        {
            rightWire = parts[2];
        }

        return (leftWire, leftValue, operation, rightWire, rightValue, outputWire);
    }
}