namespace AoC.Solutions.Solutions._2018.TimeMachine;

public class Cpu
{
    private readonly int[] _registers;

    private readonly List<(OpCode OpCode, int A, int B, int C)> _program = [];

    private int _instructionPointer;

    private int? _instructionPointerBinding;

    private int _breakAt;

    private OpCode? _breakOn;

    public Cpu(int registerCount)
    {
        _registers = new int[registerCount];
    }

    public void Run(int breakAt = -1, OpCode? breakOn = null)
    {
        _instructionPointer = 0;

        _breakAt = breakAt;

        _breakOn = breakOn;

        Continue();
    }

    public void Continue()
    {
        while (_instructionPointer < _program.Count)
        {
            if (_instructionPointerBinding.HasValue)
            {
                _registers[_instructionPointerBinding.Value] = _instructionPointer;
            }

            var instruction = _program[_instructionPointer];

            Execute(instruction.OpCode, instruction.A, instruction.B, instruction.C);

            if (_breakAt > -1 && _instructionPointer == _breakAt)
            {
                return;
            }

            if (_instructionPointerBinding.HasValue)
            {
                _instructionPointer = _registers[_instructionPointerBinding.Value];
            }

            _instructionPointer++;

            if (instruction.OpCode == _breakOn)
            {
                return;
            }
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

            _program.Add(ParseLine(line));
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

    public int GetRegister(int register)
    {
        return _registers[register];
    }

    public void Execute(OpCode opCode, int a, int b, int c)
    {
        _registers[c] = opCode switch
        {
            OpCode.Addr => _registers[a] + _registers[b],
            OpCode.Addi => _registers[a] + b,
            OpCode.Mulr => _registers[a] * _registers[b],
            OpCode.Muli => _registers[a] * b,
            OpCode.Banr => _registers[a] & _registers[b],
            OpCode.Bani => _registers[a] & b,
            OpCode.Borr => _registers[a] | _registers[b],
            OpCode.Bori => _registers[a] | b,
            OpCode.Setr => _registers[a],
            OpCode.Seti => a,
            OpCode.Gtir => a > _registers[b] ? 1 : 0,
            OpCode.Gtri => _registers[a] > b ? 1 : 0,
            OpCode.Gtrr => _registers[a] > _registers[b] ? 1 : 0,
            OpCode.Eqir => a == _registers[b] ? 1 : 0,
            OpCode.Eqri => _registers[a] == b ? 1 : 0,
            OpCode.Eqrr => _registers[a] == _registers[b] ? 1 : 0,
            _ => _registers[c]
        };
    }

    private static (OpCode OpCode, int A, int B, int C) ParseLine(string line)
    {
        var parts = line.Split(' ', StringSplitOptions.TrimEntries);

        return (ParseOpCode(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]));
    }

    private static OpCode ParseOpCode(string op) => op switch
    {
        // ReSharper disable StringLiteralTypo
        "addr" => OpCode.Addr,
        "addi" => OpCode.Addi,
        "mulr" => OpCode.Mulr,
        "muli" => OpCode.Muli,
        "banr" => OpCode.Banr,
        "bani" => OpCode.Bani,
        "borr" => OpCode.Borr,
        "bori" => OpCode.Bori,
        "setr" => OpCode.Setr,
        "seti" => OpCode.Seti,
        "gtir" => OpCode.Gtir,
        "gtri" => OpCode.Gtri,
        "gtrr" => OpCode.Gtrr,
        "eqir" => OpCode.Eqir,
        "eqri" => OpCode.Eqri,
        "eqrr" => OpCode.Eqrr,
        // ReSharper restore StringLiteralTypo
        _ => throw new ArgumentOutOfRangeException(nameof(op), op, "Unknown opcode")
    };
}