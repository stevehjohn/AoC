using System.Runtime.CompilerServices;

namespace AoC.Solutions.Solutions._2018.TimeMachine;

public sealed class Cpu
{
    private readonly int[] _registers;

    private Instruction[] _program = Array.Empty<Instruction>();

    private int _programLength;

    private int _instructionPointer;

    private int _instructionPointerBinding = -1;

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

    public void Continue()
    {
        var registers = _registers;

        var program = _program;

        var length = _programLength;

        var instructionPointerBinding = _instructionPointerBinding;

        while ((uint) _instructionPointer < (uint) length)
        {
            if (instructionPointerBinding >= 0)
            {
                registers[instructionPointerBinding] = _instructionPointer;
            }

            ref readonly var instruction = ref program[_instructionPointer];

            registers[instruction.C] = instruction.OpCode switch
            {
                OpCode.Addr => registers[instruction.A] + registers[instruction.B],
                OpCode.Addi => registers[instruction.A] + instruction.B,
                OpCode.Mulr => registers[instruction.A] * registers[instruction.B],
                OpCode.Muli => registers[instruction.A] * instruction.B,
                OpCode.Banr => registers[instruction.A] & registers[instruction.B],
                OpCode.Bani => registers[instruction.A] & instruction.B,
                OpCode.Borr => registers[instruction.A] | registers[instruction.B],
                OpCode.Bori => registers[instruction.A] | instruction.B,
                OpCode.Setr => registers[instruction.A],
                OpCode.Seti => instruction.A,
                OpCode.Gtir => instruction.A > registers[instruction.B] ? 1 : 0,
                OpCode.Gtri => registers[instruction.A] > instruction.B ? 1 : 0,
                OpCode.Gtrr => registers[instruction.A] > registers[instruction.B] ? 1 : 0,
                OpCode.Eqir => instruction.A == registers[instruction.B] ? 1 : 0,
                OpCode.Eqri => registers[instruction.A] == instruction.B ? 1 : 0,
                OpCode.Eqrr => registers[instruction.A] == registers[instruction.B] ? 1 : 0,
                _ => registers[instruction.C]
            };

            if (_breakAt > -1 && _instructionPointer == _breakAt)
            {
                _registers.AsSpan().CopyTo(registers);
                
                return;
            }

            if (instructionPointerBinding >= 0)
            {
                _instructionPointer = registers[instructionPointerBinding];
            }

            _instructionPointer++;

            if (_breakOn.HasValue && instruction.OpCode == _breakOn.Value)
            {
                _registers.AsSpan().CopyTo(registers);
                
                return;
            }
        }

        _registers.AsSpan().CopyTo(registers);
    }

    public void LoadProgram(string[] program)
    {
        var list = new List<Instruction>(program.Length);

        foreach (var line in program)
        {
            if (line.StartsWith('#'))
            {
                _instructionPointerBinding = line[4] - '0';

                continue;
            }

            list.Add(ParseLine(line));
        }

        _program = list.ToArray();

        _programLength = _program.Length;
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

    public int GetRegister(int register) => _registers[register];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Instruction ParseLine(string line)
    {
        var parts = line.Split(' ', StringSplitOptions.TrimEntries);

        return new Instruction(ParseOpCode(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static OpCode ParseOpCode(string op) => op switch
    {
        // ReSharper disable once StringLiteralTypo
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
        // ReSharper restore once StringLiteralTypo
        _ => throw new ArgumentOutOfRangeException(nameof(op), op, "Unknown opcode")
    };
}