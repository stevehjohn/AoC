﻿using AoC.Solutions.Solutions._2019.Computer;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._25;

[UsedImplicitly]
public class Part1 : Base
{
    private Room _start;

    private readonly Dictionary<string, Room> _rooms = [];

    private readonly List<string> _items = [];
    
    public override string GetAnswer()
    {
        Explore();

        TestItems();
        
        return "Unknown";
    }

    private void TestItems()
    {
        var danger = new List<string>();
        
        foreach (var item in _items)
        {
            var end = _rooms.Single(r => r.Value.Item == item).Value;

            var path = GetPath(_start.Name, end.Name);
            
            var cpu = new Cpu();
        
            cpu.Initialise(65536);

            cpu.LoadProgram(Input);
        
            cpu.Run();

            cpu.ReadString();

            foreach (var step in path)
            {
                cpu.WriteString(step);

                cpu.Run();

                cpu.ReadString();
            }
            
            cpu.WriteString($"take {item}");
            
            var response = cpu.Run(10_000);

            if (response != CpuState.AwaitingInput)
            {
                danger.Add(item);
            }
        }
        
        danger.ForEach(d => _items.Remove(d));
    }

    private List<string> GetPath(string start, string end)
    {
        var queue = new Queue<(Room Room, List<string> Path)>();
        
        queue.Enqueue((_rooms[start], []));

        while (queue.TryDequeue(out var item))
        {
            if (item.Room.Name == end)
            {
                return item.Path;
            }

            foreach (var direction in item.Room.Directions)
            {
                queue.Enqueue((direction.Value, [..item.Path, direction.Key]));
            }
        }

        return null;
    }

    private void Explore()
    {
        var cpu = new Cpu();
        
        cpu.Initialise(65536);

        cpu.LoadProgram(Input);
        
        cpu.Run();

        var response = ParseOutput(cpu.ReadString());

        var room  = new Room
        {
            Name = response.Name[3..^3],
            Item = response.Item,
            InitialDirections = response.Directions.Select(d => (d, 0)).ToList(),
            Directions = response.Directions.Select(d => new KeyValuePair<string, Room>(d, null)).ToDictionary()
        };

        if (response.Item != null)
        {
            _items.Add(response.Item);
        }

        _rooms.Add(room.Name, room);

        _start = room;

        while (true)
        {
            var directionInfo = room.InitialDirections.OrderBy(d => d.Count).ToList()[0];

            room.InitialDirections.Remove(directionInfo);

            room.InitialDirections.Add((directionInfo.Name, directionInfo.Count + 1));

            // TODO: This works, but sucks.
            if (room.InitialDirections.All(d => d.Count > 4))
            {
                break;
            }

            var direction = directionInfo.Name;
            
            cpu.WriteString(direction);

            cpu.Run();

            response = ParseOutput(cpu.ReadString());

            if (! _rooms.TryGetValue(response.Name[3..^3], out var nextRoom))
            {
                nextRoom  = new Room
                {
                    Name = response.Name[3..^3],
                    Item = response.Item,
                    InitialDirections = response.Directions.Select(d => (d, 0)).ToList(),
                    Directions = response.Directions.Select(d => new KeyValuePair<string, Room>(d, null)).ToDictionary()
                };

                if (response.Item != null)
                {
                    _items.Add(response.Item);
                }

                _rooms[nextRoom.Name] = nextRoom;
            }

            room.Directions[direction] = nextRoom;

            room = nextRoom;
        }
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