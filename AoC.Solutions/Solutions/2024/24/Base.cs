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

            if (parts[4][0] == 'z')
            {
                MaxZ = Math.Max(int.Parse(parts[4][1..]), MaxZ);
            }

            i++;
        }
    }

    protected bool GetWireValue(string wire)
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
}