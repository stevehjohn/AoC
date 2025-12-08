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
        var instruction = new Instruction(opCode, a, b, c);

        switch (instruction.OpCode)
        {
            case OpCode.Addr: _registers[instruction.C] = _registers[instruction.A] + _registers[instruction.B]; break;
            case OpCode.Addi: _registers[instruction.C] = _registers[instruction.A] + instruction.B; break;
            case OpCode.Mulr: _registers[instruction.C] = _registers[instruction.A] * _registers[instruction.B]; break;
            case OpCode.Muli: _registers[instruction.C] = _registers[instruction.A] * instruction.B; break;
            case OpCode.Banr: _registers[instruction.C] = _registers[instruction.A] & _registers[instruction.B]; break;
            case OpCode.Bani: _registers[instruction.C] = _registers[instruction.A] & instruction.B; break;
            case OpCode.Borr: _registers[instruction.C] = _registers[instruction.A] | _registers[instruction.B]; break;
            case OpCode.Bori: _registers[instruction.C] = _registers[instruction.A] | instruction.B; break;
            case OpCode.Setr: _registers[instruction.C] = _registers[instruction.A]; break;
            case OpCode.Seti: _registers[instruction.C] = instruction.A; break;
            case OpCode.Gtir: _registers[instruction.C] = instruction.A > _registers[instruction.B] ? 1 : 0; break;
            case OpCode.Gtri: _registers[instruction.C] = _registers[instruction.A] > instruction.B ? 1 : 0; break;
            case OpCode.Gtrr: _registers[instruction.C] = _registers[instruction.A] > _registers[instruction.B] ? 1 : 0; break;
            case OpCode.Eqir: _registers[instruction.C] = instruction.A == _registers[instruction.B] ? 1 : 0; break;
            case OpCode.Eqri: _registers[instruction.C] = _registers[instruction.A] == instruction.B ? 1 : 0; break;
            case OpCode.Eqrr: _registers[instruction.C] = _registers[instruction.A] == _registers[instruction.B] ? 1 : 0; break;
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

            ref readonly var instr = ref program[_instructionPointer];

            switch (instr.OpCode)
            {
                case OpCode.Addr: registers[instr.C] = registers[instr.A] + registers[instr.B]; break;
                case OpCode.Addi: registers[instr.C] = registers[instr.A] + instr.B; break;
                case OpCode.Mulr: registers[instr.C] = registers[instr.A] * registers[instr.B]; break;
                case OpCode.Muli: registers[instr.C] = registers[instr.A] * instr.B; break;
                case OpCode.Banr: registers[instr.C] = registers[instr.A] & registers[instr.B]; break;
                case OpCode.Bani: registers[instr.C] = registers[instr.A] & instr.B; break;
                case OpCode.Borr: registers[instr.C] = registers[instr.A] | registers[instr.B]; break;
                case OpCode.Bori: registers[instr.C] = registers[instr.A] | instr.B; break;
                case OpCode.Setr: registers[instr.C] = registers[instr.A]; break;
                case OpCode.Seti: registers[instr.C] = instr.A; break;
                case OpCode.Gtir: registers[instr.C] = instr.A > registers[instr.B] ? 1 : 0; break;
                case OpCode.Gtri: registers[instr.C] = registers[instr.A] > instr.B ? 1 : 0; break;
                case OpCode.Gtrr: registers[instr.C] = registers[instr.A] > registers[instr.B] ? 1 : 0; break;
                case OpCode.Eqir: registers[instr.C] = instr.A == registers[instr.B] ? 1 : 0; break;
                case OpCode.Eqri: registers[instr.C] = registers[instr.A] == instr.B ? 1 : 0; break;
                case OpCode.Eqrr: registers[instr.C] = registers[instr.A] == registers[instr.B] ? 1 : 0; break;
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

            if (_breakOn.HasValue && instr.OpCode == _breakOn.Value)
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