using AoC.Solutions.Solutions._2019.Computer;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._25;

[UsedImplicitly]
public class Part1 : Base
{
    private Room _start;

    private Room _end;

    private readonly Dictionary<string, Room> _rooms = [];

    private readonly List<string> _items = [];

    private readonly Dictionary<(string Start, string End), List<string>> _paths = [];
    
    public override string GetAnswer()
    {
        Explore();
        
        TestItems();

        return Solve();
    }

    private string Solve()
    {
        var cpu = new Cpu();
        
        cpu.Initialise(65536);

        cpu.LoadProgram(Input);
        
        cpu.Run();

        cpu.ReadString();

        var room = _start.Name;

        List<string> path;
        
        foreach (var item in _items)
        {
            var end = _rooms.Single(r => r.Value.Item == item).Value;

            path = GetPath(room, end.Name);

            foreach (var step in path)
            {
                cpu.WriteString(step);

                cpu.Run();

                cpu.ReadString();
            }

            cpu.WriteString($"take {item}");

            cpu.Run();

            room = end.Name;
        }

        path = GetPath(room, _end.Name);

        foreach (var step in path)
        {
            cpu.WriteString(step);

            cpu.Run();

            cpu.ReadString();
        }

        return PassCheckpoint(cpu, _items);
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
            else
            {
                cpu.WriteString("north");

                cpu.Run();
            
                var result = cpu.ReadString();

                if (result.Contains("You can't move!!"))
                {
                    danger.Add(item);
                }
            }
        }
        
        danger.ForEach(d => _items.Remove(d));
    }

    private List<string> GetPath(string start, string end)
    {
        if (_paths.TryGetValue((start, end), out var path))
        {
            return path;
        }

        var queue = new Queue<(Room Room, List<string> Path)>();
        
        queue.Enqueue((_rooms[start], []));

        while (queue.TryDequeue(out var item))
        {
            if (item.Room.Name == end)
            {
                _paths.Add((start, end), item.Path);
                
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
            VisitCount = [],
            Directions = []
        };

        response.Directions.ForEach(d =>
        {
            room.VisitCount.Add(d, 0);
            room.Directions.Add(d, null);
        });

        if (response.Item != null)
        {
            _items.Add(response.Item);
        }

        _rooms.Add(room.Name, room);

        _start = room;

        while (true)
        {
            if (_rooms.All(r => r.Value.Directions.All(d => d.Value != null)))
            {
                break;
            }

            var low = int.MaxValue;

            var direction = string.Empty;

            foreach (var d in room.VisitCount)
            {
                if (d.Value < low)
                {
                    low = d.Value;

                    direction = d.Key;
                }
            }

            room.VisitCount[direction]++;
            
            cpu.WriteString(direction);

            cpu.Run();

            response = ParseOutput(cpu.ReadString());

            if (! _rooms.TryGetValue(response.Name[3..^3], out var nextRoom))
            {
                nextRoom  = new Room
                {
                    Name = response.Name[3..^3],
                    Item = response.Item,
                    VisitCount = [],
                    Directions = []
                };
                
                response.Directions.ForEach(d =>
                {
                    nextRoom.VisitCount.Add(d, 0);
                    nextRoom.Directions.Add(d, null);
                });

                if (response.Item != null)
                {
                    _items.Add(response.Item);
                }

                _rooms[nextRoom.Name] = nextRoom;
            }

            room.Directions[direction] = nextRoom;

            if (room.Name == nextRoom.Name)
            {
                _end = room;
            }

            room = nextRoom;
        }
    }

    private string PassCheckpoint(Cpu cpu, List<string> items)
    {
        var i = 1;
    
        var previousCode = 0;
    
        string result;
        
        var direction = _end.Directions.Single(d => d.Value.Name == _end.Name).Key;
        
        while (true)
        {
            var greyCode = i ^ (i >> 1);
    
            var change = GetChangedBit(previousCode, greyCode);
    
            previousCode = greyCode;
    
            i++;
    
            var command = change.Value
                              ? $"drop {items[change.Index]}"
                              : $"take {items[change.Index]}";
    
            cpu.WriteString(command);
    
            cpu.Run();
    
            cpu.WriteString(direction);
    
            cpu.Run();
    
            result = cpu.ReadString();
    
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