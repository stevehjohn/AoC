namespace AoC.Solutions.Solutions._2018.TimeMachine;

public class Cpu
{
    private readonly int[] _registers;

    private readonly Dictionary<string, Action<int, int, int, int[]>> _operations = new();

    private readonly List<string> _program = new();

    private int _instructionPointer;

    private int? _instructionPointerBinding;

    public Cpu(int registerCount)
    {
        _registers = new int[registerCount];
    }

    public void Run()
    {
        _instructionPointer = 0;

        while (_instructionPointer < _program.Count)
        {
            if (_instructionPointerBinding.HasValue)
            {
                _registers[_instructionPointerBinding.Value] = _instructionPointer;
            }

            var instruction = ParseLine(_program[_instructionPointer]);

            _operations[instruction.OpCode].Invoke(instruction.A, instruction.B, instruction.C, _registers);

            if (_instructionPointerBinding.HasValue)
            {
                _instructionPointer = _registers[_instructionPointerBinding.Value];
            }

            _instructionPointer++;
        }
    }

    public void LoadProgram(string[] program)
    {
        foreach (var line in program)
        {
            if (line.StartsWith('#'))
            {
                _instructionPointerBinding = line[4] - '0';

                continue;
            }

            _program.Add(line);
        }
    }

    public void SetRegisters(int[] values)
    {
        Array.Copy(values, 0, _registers, 0, _registers.Length);
    }

    public int[] GetRegisters()
    {
        var copy = new int[_registers.Length];

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

    private static (string OpCode, int A, int B, int C) ParseLine(string line)
    {
        var parts = line.Split(' ', StringSplitOptions.TrimEntries);

        return (parts[0], int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]));
    }
}