using System.Runtime.CompilerServices;

namespace AoC.Solutions.Solutions._2018.TimeMachine;

public sealed class Cpu
{
    private readonly int[] _registers;

    private Instruction[] _program = [];

    private int _programLength;

    private int _instructionPointer;

    private int _instructionPointerBinding = -1;

    private int _divideBy256Loop = -1;

    public Cpu(int registerCount)
    {
        _registers = new int[registerCount];
    }

    public void Run(int breakAt = -1, OpCode? breakOn = null)
    {
        _instructionPointer = 0;

        Continue(breakAt, breakOn);
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

    public void Continue(int breakAt = -1, OpCode? breakOn = null)
    {
        var registers = _registers;

        var program = _program;

        var length = _programLength;

        var instructionPointerBinding = _instructionPointerBinding;

        var instructionPointer = _instructionPointer;

        while ((uint) instructionPointer < (uint) length)
        {
            if (instructionPointer == _divideBy256Loop && ! (breakAt >= _divideBy256Loop && breakAt <= _divideBy256Loop + 10))
            {
                registers[1] = registers[3] / 256;
                registers[3] = registers[1];
                registers[4] = 1;

                instructionPointer = 8;

                continue;
            }

            if (instructionPointerBinding >= 0)
            {
                registers[instructionPointerBinding] = instructionPointer;
            }

            ref readonly var instruction = ref program[instructionPointer];

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

            if (breakAt > -1 && instructionPointer == breakAt)
            {
                _instructionPointer = instructionPointer;

                return;
            }

            if (instructionPointerBinding >= 0)
            {
                instructionPointer = registers[instructionPointerBinding];
            }

            instructionPointer++;

            // ReSharper disable once PossibleInvalidOperationException
            if (breakOn.HasValue && instruction.OpCode == breakOn.Value)
            {
                _instructionPointer = instructionPointer;

                return;
            }
        }

        _instructionPointer = instructionPointer;
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

        _program = [.. list];

        _programLength = _program.Length;

        _divideBy256Loop = FindDivideBy256Loop();
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

    private int FindDivideBy256Loop()
    {
        for (var i = 0; i <= _programLength - 11; i++)
        {
            if (IsDivideBy256Loop(i))
            {
                return i;
            }
        }

        return -1;
    }

    private bool IsDivideBy256Loop(int i)
    {
        return
            Is(i, OpCode.Seti, 0, 9, 1) &&
            Is(i + 1, OpCode.Addi, 1, 1, 4) &&
            Is(i + 2, OpCode.Muli, 4, 256, 4) &&
            Is(i + 3, OpCode.Gtrr, 4, 3, 4) &&
            Is(i + 4, OpCode.Addr, 4, 2, 2) &&
            Is(i + 5, OpCode.Addi, 2, 1, 2) &&
            Is(i + 6, OpCode.Seti, 25, 4, 2) &&
            Is(i + 7, OpCode.Addi, 1, 1, 1) &&
            Is(i + 8, OpCode.Seti, 17, 2, 2) &&
            Is(i + 9, OpCode.Setr, 1, 6, 3) &&
            Is(i + 10, OpCode.Seti, 7, 8, 2);
    }

    private bool Is(int i, OpCode opCode, int a, int b, int c)
    {
        ref readonly var instruction = ref _program[i];

        return instruction.OpCode == opCode &&
               instruction.A == a &&
               instruction.B == b &&
               instruction.C == c;
    }

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