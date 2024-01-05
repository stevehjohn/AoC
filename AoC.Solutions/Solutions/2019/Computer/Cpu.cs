using JetBrains.Annotations;
using System.Reflection;
using System.Text;
using AoC.Solutions.Exceptions;

namespace AoC.Solutions.Solutions._2019.Computer;

public class Cpu
{
    public long[] Memory;

    public readonly Queue<long> UserInput = new();

    public readonly Queue<long> UserOutput = new();

    private readonly Dictionary<int, Instruction> _opCodes = new();

    private long _programCounter;

    private long _relativeBase;

    private long[] _operands;

    private byte[] _modes;

    private int _additionalMemory;

    public void Initialise(int additionalMemory = 0)
    {
        AddAllOpCodes();

        _additionalMemory = additionalMemory;
    }

    public void LoadProgram(string[] input)
    {
        var program = input[0].Split(',').Select(long.Parse).ToArray();

        Memory = new long[program.Length + _additionalMemory];

        Array.Copy(program, Memory, program.Length);

        _programCounter = 0;

        _relativeBase = 0;
    }

    public CpuState Run(int maxCycles = int.MaxValue)
    {
        while (maxCycles > 0)
        {
            var opCodeWithFlags = Memory[_programCounter].ToString().PadLeft(5, '0');

            var opCode = int.Parse(opCodeWithFlags[3..]);

            if (opCode == 99)
            {
                return CpuState.Halted;
            }

            var instruction = _opCodes[opCode];

            _operands = Memory[((int) _programCounter + 1)..((int) _programCounter + instruction.Length)];

            _modes = opCodeWithFlags[..3].Select(c => (byte) (c - '0')).Reverse().ToArray();

            switch (instruction.Execute())
            {
                case OperationState.CompleteJumped:
                    continue;
                case OperationState.CompleteAdvanceCounter:
                    _programCounter += instruction.Length;
                    break;
                case OperationState.AwaitingInput:
                    return CpuState.AwaitingInput;
            }

            maxCycles--;
        }

        return CpuState.MaxCyclesExceeded;
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
        
        while (UserOutput.TryDequeue(out var c))
        {
            builder.Append((char) c);
        }

        return builder.ToString();
    }

    private void AddAllOpCodes()
    {
        var methods = GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic).Where(m => m.Name.StartsWith("OpCodes"));

        foreach (var method in methods)
        {
            method.Invoke(this, null);
        }
    }

    [UsedImplicitly]
    private void OpCodesPart2()
    {
        _opCodes.Add(1, new Instruction
                        {
                            Length = 4,
                            Execute = () =>
                            {
                                SetMemory(2, GetOperand(0) + GetOperand(1));

                                return OperationState.CompleteAdvanceCounter;
                            }
                        });

        _opCodes.Add(2, new Instruction
                        {
                            Length = 4,
                            Execute = () =>
                            {
                                SetMemory(2, GetOperand(0) * GetOperand(1));

                                return OperationState.CompleteAdvanceCounter;
                            }
                        });
    }

    [UsedImplicitly]
    private void OpCodesPart5()
    {
        _opCodes.Add(3, new Instruction
        {
            Length = 2,
            Execute = () =>
            {
                if (UserInput.Count == 0)
                {
                    return OperationState.AwaitingInput;
                }

                SetMemory(0, UserInput.Dequeue());

                return OperationState.CompleteAdvanceCounter;
            }
        });

        _opCodes.Add(4, new Instruction
        {
            Length = 2,
            Execute = () =>
            {
                UserOutput.Enqueue(GetOperand(0));

                return OperationState.CompleteAdvanceCounter;
            }
        });

        _opCodes.Add(5, new Instruction
        {
            Length = 3,
            Execute = () =>
            {
                if (GetOperand(0) != 0)
                {
                    _programCounter = GetOperand(1);

                    return OperationState.CompleteJumped;
                }

                return OperationState.CompleteAdvanceCounter;
            }
        });

        _opCodes.Add(6, new Instruction
        {
            Length = 3,
            Execute = () =>
            {
                if (GetOperand(0) == 0)
                {
                    _programCounter = GetOperand(1);

                    return OperationState.CompleteJumped;
                }

                return OperationState.CompleteAdvanceCounter;
            }
        });

        _opCodes.Add(7, new Instruction
        {
            Length = 4,
            Execute = () =>
            {
                SetMemory(2, GetOperand(0) < GetOperand(1) ? 1 : 0);

                return OperationState.CompleteAdvanceCounter;
            }
        });

        _opCodes.Add(8, new Instruction
        {
            Length = 4,
            Execute = () =>
            {
                SetMemory(2, GetOperand(0) == GetOperand(1) ? 1 : 0);

                return OperationState.CompleteAdvanceCounter;
            }
        });
    }


    [UsedImplicitly]
    private void OpCodesPart9()
    {
        _opCodes.Add(9, new Instruction
                        {
                            Length = 2,
                            Execute = () =>
                            {
                                _relativeBase += GetOperand(0);

                                return OperationState.CompleteAdvanceCounter;
                            }
                        });
    }

    private long GetOperand(int index)
    {
        switch (_modes[index])
        {
            case 0:
                return Memory[_operands[index]];
            case 1:
                return _operands[index];
            case 2:
                return Memory[_relativeBase + _operands[index]];
        }

        throw new PuzzleException($"Unknown read operand mode {_modes[index]}.");
    }

    private void SetMemory(int addressOperandIndex, long value)
    {
        switch (_modes[addressOperandIndex])
        {
            case 0:
                Memory[_operands[addressOperandIndex]] = value;
                return;
            case 2:
                Memory[_relativeBase + _operands[addressOperandIndex]] = value;
                return;
        }

        throw new PuzzleException($"Unknown write operand mode {_modes[addressOperandIndex]}.");
    }
}