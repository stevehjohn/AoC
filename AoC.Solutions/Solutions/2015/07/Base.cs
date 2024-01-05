using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2015._07;

public abstract class Base : Solution
{
    public override string Description => "Logic gates";

    private readonly Dictionary<string, (string LeftWire, ushort LeftValue, string Operation, string RightWire, ushort RightValue, string OutputWire)> _wires = new();

    protected readonly Dictionary<string, ushort> Values = new();

    protected ushort FindOutputValue(string outputWire)
    {
        if (Values.TryGetValue(outputWire, out var value))
        {
            return value;
        }

        var operation = _wires[outputWire];

        ushort leftValue;

        ushort rightValue;

        ushort result = 0;

        // ReSharper disable StringLiteralTypo
        switch (operation.Operation)
        {
            case null:
                if (operation.LeftWire == null)
                {
                    result = operation.LeftValue;

                    break;
                }

                result = FindOutputValue(operation.LeftWire);

                break;
            case "NOT":
                if (operation.RightWire == null)
                {
                    result = (ushort) (operation.RightValue ^ 0xFFFF);

                    break;
                }

                result = (ushort) (FindOutputValue(operation.RightWire) ^ 0xFFFF);

                break;
            case "AND":
                leftValue = operation.LeftWire == null
                                ? operation.LeftValue
                                : FindOutputValue(operation.LeftWire);

                rightValue = operation.RightWire == null
                                 ? operation.RightValue
                                 : FindOutputValue(operation.RightWire);

                result = (ushort) (leftValue & rightValue);

                break;
            case "OR":
                leftValue = operation.LeftWire == null
                                ? operation.LeftValue
                                : FindOutputValue(operation.LeftWire);

                rightValue = operation.RightWire == null
                                 ? operation.RightValue
                                 : FindOutputValue(operation.RightWire);

                result = (ushort) (leftValue | rightValue);

                break;
            case "LSHIFT":
                leftValue = operation.LeftWire == null
                                ? operation.LeftValue
                                : FindOutputValue(operation.LeftWire);

                rightValue = operation.RightWire == null
                                 ? operation.RightValue
                                 : FindOutputValue(operation.RightWire);

                result = (ushort) (leftValue << rightValue);

                break;
            case "RSHIFT":
                leftValue = operation.LeftWire == null
                                ? operation.LeftValue
                                : FindOutputValue(operation.LeftWire);

                rightValue = operation.RightWire == null
                                 ? operation.RightValue
                                 : FindOutputValue(operation.RightWire);

                result = (ushort) (leftValue >> rightValue);

                break;
        }
        // ReSharper restore StringLiteralTypo

        Values.Add(outputWire, result);

        return result;
    }

    protected void ParseInput()
    {
        foreach (var line in Input)
        {
            var data = ParseLine(line);

            _wires.Add(data.OutputWire, data);
        }
    }

    private static (string LeftWire, ushort LeftValue, string Operation, string RightWire, ushort RightValue, string OutputWire) ParseLine(string line)
    {
        var parts = line.Split(' ', StringSplitOptions.TrimEntries);

        var outputWire = parts[^1];

        switch (parts.Length)
        {
            case 3 when ushort.TryParse(parts[0], out var value):
                return (null, value, null, null, 0, outputWire);
            case 3:
                return (parts[0], 0, null, null, 0, outputWire);
            case 4 when ushort.TryParse(parts[1], out var value):
                return (null, 0, parts[0], null, value, outputWire);
            case 4:
                return (null, 0, parts[0], parts[1], 0, outputWire);
        }

        var operation = parts[1];

        string leftWire = null;

        string rightWire = null;

        if (! ushort.TryParse(parts[0], out var leftValue))
        {
            leftWire = parts[0];
        }

        if (! ushort.TryParse(parts[2], out var rightValue))
        {
            rightWire = parts[2];
        }

        return (leftWire, leftValue, operation, rightWire, rightValue, outputWire);
    }
}