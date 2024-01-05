namespace AoC.Solutions.Solutions._2018.TimeMachine;

public class Cpu
{
    private readonly int[] _registers;

    private readonly List<(string OpCode, int A, int B, int C)> _program = [];

    private int _instructionPointer;

    private int? _instructionPointerBinding;

    private int _breakAt;

    private string _breakOn;

    public Cpu(int registerCount)
    {
        _registers = new int[registerCount];
    }

    public void Run(int breakAt = -1, string breakOn = "")
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

    public void Execute(string opCode, int a, int b, int c)
    {
        // ReSharper disable StringLiteralTypo
        _registers[c] = opCode switch
        {
            "addr" => _registers[a] + _registers[b],
            "addi" => _registers[a] + b,
            "mulr" => _registers[a] * _registers[b],
            "muli" => _registers[a] * b,
            "banr" => _registers[a] & _registers[b],
            "bani" => _registers[a] & b,
            "borr" => _registers[a] | _registers[b],
            "bori" => _registers[a] | b,
            "setr" => _registers[a],
            "seti" => a,
            "gtir" => a > _registers[b] ? 1 : 0,
            "gtri" => _registers[a] > b ? 1 : 0,
            "gtrr" => _registers[a] > _registers[b] ? 1 : 0,
            "eqir" => a == _registers[b] ? 1 : 0,
            "eqri" => _registers[a] == b ? 1 : 0,
            "eqrr" => _registers[a] == _registers[b] ? 1 : 0,
            _ => _registers[c]
        };
        // ReSharper restore StringLiteralTypo
    }

    private static (string OpCode, int A, int B, int C) ParseLine(string line)
    {
        var parts = line.Split(' ', StringSplitOptions.TrimEntries);

        return (parts[0], int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]));
    }
}