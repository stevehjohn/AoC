using System.Runtime.CompilerServices;

namespace AoC.Solutions.Solutions._2018.TimeMachine;

public sealed class Cpu
{
    private readonly int[] _registers;

    private Instruction[] _program = [];

    private int _programLength;

    private int _instructionPointer;

    private int _instructionPointerBinding = -1;

    private int _breakAt;

    private OpCode? _breakOn;

    private bool _hasBreakOn;

    public Cpu(int registerCount)
    {
        _registers = new int[registerCount];
    }

    public void Run(int breakAt = -1, OpCode? breakOn = null)
    {
        _instructionPointer = 0;

        _breakAt = breakAt;

        _breakOn = breakOn;

        _hasBreakOn = _breakOn.HasValue;

        Continue();
    }

    public void Execute(OpCode opCode, int a, int b, int c)
    {
        // ReSharper disable once ConvertSwitchStatementToSwitchExpression - Statement is faster in this instance
        // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
        switch (opCode)
        {
            case OpCode.Addr: _registers[c] = _registers[a] + _registers[b]; break;
            case OpCode.Addi: _registers[c] = _registers[a] + b; break;
            case OpCode.Mulr: _registers[c] = _registers[a] * _registers[b]; break;
            case OpCode.Muli: _registers[c] = _registers[a] * b; break;
            case OpCode.Banr: _registers[c] = _registers[a] & _registers[b]; break;
            case OpCode.Bani: _registers[c] = _registers[a] & b; break;
            case OpCode.Borr: _registers[c] = _registers[a] | _registers[b]; break;
            case OpCode.Bori: _registers[c] = _registers[a] | b; break;
            case OpCode.Setr: _registers[c] = _registers[a]; break;
            case OpCode.Seti: _registers[c] = a; break;
            case OpCode.Gtir: _registers[c] = a > _registers[b] ? 1 : 0; break;
            case OpCode.Gtri: _registers[c] = _registers[a] > b ? 1 : 0; break;
            case OpCode.Gtrr: _registers[c] = _registers[a] > _registers[b] ? 1 : 0; break;
            case OpCode.Eqir: _registers[c] = a == _registers[b] ? 1 : 0; break;
            case OpCode.Eqri: _registers[c] = _registers[a] == b ? 1 : 0; break;
            case OpCode.Eqrr: _registers[c] = _registers[a] == _registers[b] ? 1 : 0; break;
        }
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

            // ReSharper disable once ConvertSwitchStatementToSwitchExpression - Statement is faster in this instance
            // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
            switch (instruction.OpCode)
            {
                case OpCode.Addr: registers[instruction.C] = registers[instruction.A] + registers[instruction.B]; break;
                case OpCode.Addi: registers[instruction.C] = registers[instruction.A] + instruction.B; break;
                case OpCode.Mulr: registers[instruction.C] = registers[instruction.A] * registers[instruction.B]; break;
                case OpCode.Muli: registers[instruction.C] = registers[instruction.A] * instruction.B; break;
                case OpCode.Banr: registers[instruction.C] = registers[instruction.A] & registers[instruction.B]; break;
                case OpCode.Bani: registers[instruction.C] = registers[instruction.A] & instruction.B; break;
                case OpCode.Borr: registers[instruction.C] = registers[instruction.A] | registers[instruction.B]; break;
                case OpCode.Bori: registers[instruction.C] = registers[instruction.A] | instruction.B; break;
                case OpCode.Setr: registers[instruction.C] = registers[instruction.A]; break;
                case OpCode.Seti: registers[instruction.C] = instruction.A; break;
                case OpCode.Gtir: registers[instruction.C] = instruction.A > registers[instruction.B] ? 1 : 0; break;
                case OpCode.Gtri: registers[instruction.C] = registers[instruction.A] > instruction.B ? 1 : 0; break;
                case OpCode.Gtrr: registers[instruction.C] = registers[instruction.A] > registers[instruction.B] ? 1 : 0; break;
                case OpCode.Eqir: registers[instruction.C] = instruction.A == registers[instruction.B] ? 1 : 0; break;
                case OpCode.Eqri: registers[instruction.C] = registers[instruction.A] == instruction.B ? 1 : 0; break;
                case OpCode.Eqrr: registers[instruction.C] = registers[instruction.A] == registers[instruction.B] ? 1 : 0; break;
            }

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

            // ReSharper disable once PossibleInvalidOperationException
            if (_hasBreakOn && instruction.OpCode == _breakOn.Value)
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