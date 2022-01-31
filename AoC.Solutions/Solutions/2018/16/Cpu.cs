namespace AoC.Solutions.Solutions._2018._16;

public class Cpu
{
    private readonly int[] _registers = new int[4];

    private readonly Dictionary<string, Action<int, int, int, int[]>> _operations = new();

    public void SetRegisters(int[] values)
    {
        Array.Copy(values, 0, _registers, 0, _registers.Length);
    }

    public int[] GetRegisters()
    {
        var copy = new int[4];

        Array.Copy(_registers, 0, copy, 0, _registers.Length);

        return copy;
    }

    public void Execute(string opCode, int a, int b, int c)
    {
        _operations[opCode].Invoke(a, b, c, _registers);
    }

    public void Initialise()
    {
        // ReSharper disable StringLiteralTypo
        _operations.Add("addr", (a, b, c, registers) => registers[c] = registers[a] + registers[b]);

        _operations.Add("addi", (a, b, c, registers) => registers[c] = registers[a] + b);

        _operations.Add("mulr", (a, b, c, registers) => registers[c] = registers[a] * registers[b]);

        _operations.Add("muli", (a, b, c, registers) => registers[c] = registers[a] * b);

        _operations.Add("banr", (a, b, c, registers) => registers[c] = registers[a] & registers[b]);

        _operations.Add("bani", (a, b, c, registers) => registers[c] = registers[a] & b);

        _operations.Add("borr", (a, b, c, registers) => registers[c] = registers[a] | registers[b]);

        _operations.Add("bori", (a, b, c, registers) => registers[c] = registers[a] | b);

        _operations.Add("setr", (a, _, c, registers) => registers[c] = registers[a]);

        _operations.Add("seti", (a, _, c, registers) => registers[c] = a);

        _operations.Add("gtir", (a, b, c, registers) => registers[c] = a > registers[b] ? 1 : 0);

        _operations.Add("gtri", (a, b, c, registers) => registers[c] = registers[a] > b ? 1 : 0);

        _operations.Add("gtrr", (a, b, c, registers) => registers[c] = registers[a] > registers[b] ? 1 : 0);

        _operations.Add("eqir", (a, b, c, registers) => registers[c] = a == registers[b] ? 1 : 0);

        _operations.Add("eqri", (a, b, c, registers) => registers[c] = registers[a] == b ? 1 : 0);

        _operations.Add("eqrr", (a, b, c, registers) => registers[c] = registers[a] == registers[b] ? 1 : 0);
        // ReSharper restore StringLiteralTypo
    }
}