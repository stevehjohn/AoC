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

        while ((uint)_instructionPointer < (uint)length)
        {
            if (instructionPointerBinding >= 0)
            {
                registers[instructionPointerBinding] = _instructionPointer;
            }

            ref readonly var instr = ref program[_instructionPointer];

            registers[instr.C] = instr.OpCode switch
            {
                OpCode.Addr => registers[instr.A] + registers[instr.B],
                OpCode.Addi => registers[instr.A] + instr.B,
                OpCode.Mulr => registers[instr.A] * registers[instr.B],
                OpCode.Muli => registers[instr.A] * instr.B,
                OpCode.Banr => registers[instr.A] & registers[instr.B],
                OpCode.Bani => registers[instr.A] & instr.B,
                OpCode.Borr => registers[instr.A] | registers[instr.B],
                OpCode.Bori => registers[instr.A] | instr.B,
                OpCode.Setr => registers[instr.A],
                OpCode.Seti => instr.A,
                OpCode.Gtir => instr.A > registers[instr.B] ? 1 : 0,
                OpCode.Gtri => registers[instr.A] > instr.B ? 1 : 0,
                OpCode.Gtrr => registers[instr.A] > registers[instr.B] ? 1 : 0,
                OpCode.Eqir => instr.A == registers[instr.B] ? 1 : 0,
                OpCode.Eqri => registers[instr.A] == instr.B ? 1 : 0,
                OpCode.Eqrr => registers[instr.A] == registers[instr.B] ? 1 : 0,
                _ => registers[instr.C]
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
