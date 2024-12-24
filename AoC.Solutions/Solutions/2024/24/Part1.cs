using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._24;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var result = GetOutputValue();
        
        return result.ToString();
    }

    private ulong GetOutputValue()
    {
        var result = 0UL;

        for (var i = 0; i <= MaxZ; i++)
        {
            if (GetWireValue($"z{i:D2}"))
            {
                result |= 1UL << i;
            }
        }

        return result;
    }

    private bool GetWireValue(string wire)
    {
        return GetGateValue(Gates[wire]);
    }

    private bool GetGateValue(Gate gate)
    {
        var left = gate.Left[0] is 'x' or 'y' 
            ? Wires[gate.Left] 
            : GetWireValue(gate.Left);

        var right = gate.Left[0] is 'x' or 'y' 
            ? Wires[gate.Right] 
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