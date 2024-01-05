using AoC.Solutions.Solutions._2019.Computer;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._25;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly Cpu _cpu = new();

    // TODO: Make the program play this part also (look at day 15's exploration algorithm?).
    private readonly List<string> _commands =
    [
        "east",
        "take spool of cat6",
        "north",
        "north",
        "take hypercube",
        "south",
        "south",
        "west",
        "south",
        "south",
        "south",
        "east",
        "east",
        "take planetoid",
        "west",
        "west",
        "north",
        "north",
        "east",
        "take space heater",
        "west",
        "north",
        "north",
        "take festive hat",
        "west",
        "take dark matter",
        "north",
        "east",
        "take semiconductor",
        "east",
        "take sand",
        "north",
        "inv"
    ];

    public override string GetAnswer()
    {
        _cpu.Initialise(65536);

        _cpu.LoadProgram(Input);

        _cpu.Run();

        var output = ParseOutput(_cpu.ReadString());

        foreach (var command in _commands)
        {
            _cpu.WriteString(command);
            
            _cpu.Run();

            var response = _cpu.ReadString();

            output = ParseOutput(response);
        }

        return PassCheckpoint(output.Items);
    }

    private string PassCheckpoint(List<string> items)
    {
        var i = 1;

        var previousCode = 0;

        string result;

        while (true)
        {
            var greyCode = i ^ (i >> 1);

            var change = GetChangedBit(previousCode, greyCode);

            previousCode = greyCode;

            i++;

            var command = change.Value
                              ? $"drop {items[change.Index]}"
                              : $"take {items[change.Index]}";

            _cpu.WriteString(command);

            _cpu.Run();

            _cpu.WriteString("west");

            _cpu.Run();

            result = _cpu.ReadString();

            if (result.Contains("Analysis complete! You may proceed."))
            {
                break;
            }
        }

        var index = result.IndexOf("typing", StringComparison.InvariantCultureIgnoreCase);

        result = result[(index + 7)..];

        result = result[..result.IndexOf(' ')];

        return result;
    }

    private static (int Index, bool Value) GetChangedBit(int code1, int code2)
    {
        var index = 0;

        var mask = 1;

        while ((code1 & mask) == (code2 & mask))
        {
            mask <<= 1;

            index++;
        }

        return (index, (code2 & mask) > 0);
    }

    private static (string Room, List<string> Directions, List<string> Items) ParseOutput(string output)
    {
        var lines = output.Split('\n');

        var room = string.Empty;

        var directions = new List<string>();

        var items = new List<string>();

        var mode = 0;

        foreach (var line in lines)
        {
            if (line.StartsWith("== "))
            {
                room = line;

                continue;
            }

            if (line == "Doors here lead:")
            {
                mode = 1;

                continue;
            }

            if (line == "Items here:" || line == "Items in your inventory:")
            {
                mode = 2;

                continue;
            }

            if (mode > 0 && ! line.StartsWith("- "))
            {
                mode = 0;

                continue;
            }

            if (mode == 1)
            {
                directions.Add(line.Substring(2));

                continue;
            }

            if (mode == 2)
            {
                items.Add(line.Substring(2));
            }
        }

        return (room, directions, items);
    }
}