using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2024._24;

public abstract class Base : Solution
{
    public override string Description => "Crossed wires";

    private int _maxZ;

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

            if (parts[4][0] == 'z')
            {
                _maxZ = Math.Max(int.Parse(parts[4][1..]), _maxZ);
            }

            i++;
        }
    }

    private bool GetWireValue(string wire)
    {
        return GetGateValue(_gates[wire]);
    }

    private bool GetGateValue(Gate gate)
    {
        var left = gate.Left[0] is 'x' or 'y' 
            ? _wires[gate.Left] 
            : GetWireValue(gate.Left);

        var right = gate.Left[0] is 'x' or 'y' 
            ? _wires[gate.Right] 
            : GetWireValue(gate.Right);

        switch (gate.Type)
        {
            case Type.AND:
                return left && right;
            
            case Type.OR:
                return left || right;

            case Type.XOR:
            default:
                return left != right;
        }
    }

    protected ulong GetNumber(char prefix)
    {
        var result = 0UL;

        var max = prefix == 'z' ? _maxZ : _maxZ - 1;
        
        for (var i = 0; i <= max; i++)
        {
            if (GetWireValue($"{prefix}{i:D2}"))
            {
                result |= 1UL << i;
            }
        }

        return result;
    }
}