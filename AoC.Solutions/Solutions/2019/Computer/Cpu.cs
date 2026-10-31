
using System.Runtime.CompilerServices;
using System.Text;
using AoC.Solutions.Exceptions;

namespace AoC.Solutions.Solutions._2019.Computer;

public class Cpu
{
    public long[] Memory;

    public readonly Queue<long> UserInput = new();
    public readonly Queue<long> UserOutput = new();

    private long[] _originalMemory;
    private long _programCounter;
    private long _relativeBase;
    private int _additionalMemory;

    public void Initialise(int additionalMemory = 0)
    {
        _additionalMemory = additionalMemory;
    }

    public void LoadProgram(string[] input)
    {
        var program = input[0].Split(',').Select(long.Parse).ToArray();

        Memory = new long[program.Length + _additionalMemory];
        _originalMemory = new long[Memory.Length];

        Array.Copy(program, Memory, program.Length);
        Array.Copy(program, _originalMemory, program.Length);

        _programCounter = 0;
        _relativeBase = 0;
    }

    public void Reset()
    {
        Array.Copy(_originalMemory, Memory, _originalMemory.Length);

        _programCounter = 0;
        _relativeBase = 0;
    }

    public CpuState Run(int maxCycles = int.MaxValue)
    {
        var memory = Memory;
        var pc = _programCounter;
        var relativeBase = _relativeBase;

        while (maxCycles > 0)
        {
            var instruction = memory[pc];

            var opcode = (int)(instruction % 100);
            var mode1 = (int)(instruction / 100 % 10);
            var mode2 = (int)(instruction / 1000 % 10);
            var mode3 = (int)(instruction / 10000 % 10);

            switch (opcode)
            {
                case 1:
                {
                    var a = Read(memory, memory[pc + 1], mode1, relativeBase);
                    var b = Read(memory, memory[pc + 2], mode2, relativeBase);
                    var address = Address(memory[pc + 3], mode3, relativeBase);

                    memory[address] = a + b;
                    pc += 4;
                    break;
                }

                case 2:
                {
                    var a = Read(memory, memory[pc + 1], mode1, relativeBase);
                    var b = Read(memory, memory[pc + 2], mode2, relativeBase);
                    var address = Address(memory[pc + 3], mode3, relativeBase);

                    memory[address] = a * b;
                    pc += 4;
                    break;
                }

                case 3:
                {
                    if (!UserInput.TryDequeue(out var value))
                    {
                        _programCounter = pc;
                        _relativeBase = relativeBase;

                        return CpuState.AwaitingInput;
                    }

                    var address = Address(memory[pc + 1], mode1, relativeBase);

                    memory[address] = value;
                    pc += 2;
                    break;
                }

                case 4:
                {
                    var value = Read(memory, memory[pc + 1], mode1, relativeBase);

                    UserOutput.Enqueue(value);
                    pc += 2;
                    break;
                }

                case 5:
                {
                    var condition = Read(memory, memory[pc + 1], mode1, relativeBase);
                    var destination = Read(memory, memory[pc + 2], mode2, relativeBase);

                    pc = condition != 0 ? destination : pc + 3;
                    break;
                }

                case 6:
                {
                    var condition = Read(memory, memory[pc + 1], mode1, relativeBase);
                    var destination = Read(memory, memory[pc + 2], mode2, relativeBase);

                    pc = condition == 0 ? destination : pc + 3;
                    break;
                }

                case 7:
                {
                    var a = Read(memory, memory[pc + 1], mode1, relativeBase);
                    var b = Read(memory, memory[pc + 2], mode2, relativeBase);
                    var address = Address(memory[pc + 3], mode3, relativeBase);

                    memory[address] = a < b ? 1 : 0;
                    pc += 4;
                    break;
                }

                case 8:
                {
                    var a = Read(memory, memory[pc + 1], mode1, relativeBase);
                    var b = Read(memory, memory[pc + 2], mode2, relativeBase);
                    var address = Address(memory[pc + 3], mode3, relativeBase);

                    memory[address] = a == b ? 1 : 0;
                    pc += 4;
                    break;
                }

                case 9:
                {
                    relativeBase += Read(
                        memory,
                        memory[pc + 1],
                        mode1,
                        relativeBase);

                    pc += 2;
                    break;
                }

                case 99:
                {
                    _programCounter = pc;
                    _relativeBase = relativeBase;

                    return CpuState.Halted;
                }

                default:
                    throw new PuzzleException($"Unknown opcode {opcode}.");
            }

            maxCycles--;
        }

        _programCounter = pc;
        _relativeBase = relativeBase;

        return CpuState.MaxCyclesExceeded;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static long Read(
        long[] memory,
        long parameter,
        int mode,
        long relativeBase)
    {
        return mode switch
        {
            0 => memory[parameter],
            1 => parameter,
            2 => memory[relativeBase + parameter],
            _ => throw new PuzzleException($"Unknown read operand mode {mode}.")
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static long Address(
        long parameter,
        int mode,
        long relativeBase)
    {
        return mode switch
        {
            0 => parameter,
            2 => relativeBase + parameter,
            _ => throw new PuzzleException($"Unknown write operand mode {mode}.")
        };
    }

    public void WriteString(string input)
    {
        foreach (var c in input)
        {
            UserInput.Enqueue(c);
        }

        UserInput.Enqueue('\n');
    }

    public string ReadString()
    {
        var builder = new StringBuilder();

        while (UserOutput.TryDequeue(out var value))
        {
            builder.Append((char)value);
        }

        return builder.ToString();
    }
}
