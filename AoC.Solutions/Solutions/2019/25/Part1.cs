using AoC.Solutions.Solutions._2019.Computer;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._25;

[UsedImplicitly]
public class Part1 : Base
{
    private Room _start;

    private List<Room> _rooms = [];
    
    public override string GetAnswer()
    {
        Explore();

        return "Unknown";
    }

    private void Explore()
    {
        var cpu = new Cpu();
        
        cpu.Initialise(65536);

        cpu.Run();

        var data = ParseOutput(cpu.ReadString());

        _start = new Room
        {
            Name = data.Name,
            Item = data.Item
        };
    }

    // private string PassCheckpoint(List<string> items)
    // {
    //     var i = 1;
    //
    //     var previousCode = 0;
    //
    //     string result;
    //
    //     while (true)
    //     {
    //         var greyCode = i ^ (i >> 1);
    //
    //         var change = GetChangedBit(previousCode, greyCode);
    //
    //         previousCode = greyCode;
    //
    //         i++;
    //
    //         var command = change.Value
    //                           ? $"drop {items[change.Index]}"
    //                           : $"take {items[change.Index]}";
    //
    //         _cpu.WriteString(command);
    //
    //         _cpu.Run();
    //
    //         _cpu.WriteString("west");
    //
    //         _cpu.Run();
    //
    //         result = _cpu.ReadString();
    //
    //         if (result.Contains("Analysis complete! You may proceed."))
    //         {
    //             break;
    //         }
    //     }
    //
    //     var index = result.IndexOf("typing", StringComparison.InvariantCultureIgnoreCase);
    //
    //     result = result[(index + 7)..];
    //
    //     result = result[..result.IndexOf(' ')];
    //
    //     return result;
    // }

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

    private static (string Name, List<string> Directions, string Item) ParseOutput(string output)
    {
        var lines = output.Split('\n');

        var room = string.Empty;

        var directions = new List<string>();

        string item = null;

        var mode = 0;

        foreach (var line in lines)
        {
            if (line.StartsWith("== "))
            {
                room = line;

                continue;
            }

            switch (line)
            {
                case "Doors here lead:":
                    mode = 1;

                    continue;
                case "Items here:":
                case "Items in your inventory:":
                    mode = 2;

                    continue;
            }

            switch (mode)
            {
                case > 0 when ! line.StartsWith("- "):
                    mode = 0;

                    continue;
                case 1:
                    directions.Add(line.Substring(2));

                    continue;
                case 2:
                    item = line.Substring(2);
                    break;
            }
        }

        return (room, directions, item);
    }
}