using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._24;

public abstract class Base : Solution
{
    public override string Description => "Crossed wires";

    protected int MaxZ;

    private readonly Dictionary<string, bool> _wires = [];

    private readonly Dictionary<string, Gate> _gates = [];
    
    protected void ParseInput()
    {
        var i = 0;

        var line = Input[0];
        
        while (! string.IsNullOrWhiteSpace(line))
        {
            _wires.Add(line[..3], line[5] == '1');

            i++;

            line = Input[i];
        }

        i++;

        while (i < Input.Length)
        {
            line = Input[i];

            var parts = line.Split(' ');

            var gate = new Gate(parts[0], parts[2], Enum.Parse<Type>(parts[1]));
            
            _gates.Add(parts[4], gate);
            
            i++;
        }
    }

    protected bool GetValue(string wire)
    {
        return false;
    }
}