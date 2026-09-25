using System.Text;

namespace AoC.Solutions.Solutions._2016.Common;

public static class Cpu
{
    public static int RunProgram(string[] input, Dictionary<char, int> initialRegisters)
    {
        var program = ParseProgram(input);

        Span<int> registers = stackalloc int[4];

        foreach (var register in initialRegisters)
        {
            registers[register.Key - 'a'] = register.Value;
        }

        var programCounter = 0;

        var output = new StringBuilder();

        while ((uint) programCounter < (uint) program.Length)
        {
            ref var instruction = ref program[programCounter];

            switch (instruction.OpCode)
            {
                case OpCode.Cpy:
                    if (instruction.B.IsRegister)
                    {
                        registers[instruction.B.Value] = GetValue(instruction.A, registers);
                    }

                    break;

                case OpCode.Inc:
                    if (instruction.A.IsRegister)
                    {
                        registers[instruction.A.Value]++;
                    }

                    break;

                case OpCode.Dec:
                    if (instruction.A.IsRegister)
                    {
                        registers[instruction.A.Value]--;
                    }

                    break;

                case OpCode.Jnz:
                    if (GetValue(instruction.A, registers) != 0)
                    {
                        programCounter += GetValue(instruction.B, registers);

                        continue;
                    }

                    break;

                case OpCode.Tgl:
                {
                    var target = programCounter + GetValue(instruction.A, registers);

                    if ((uint) target >= (uint) program.Length)
                    {
                        break;
                    }

                    ref var toToggle = ref program[target];

                    toToggle.OpCode = toToggle.OpCode switch
                    {
                        OpCode.Inc => OpCode.Dec,
                        OpCode.Dec or OpCode.Tgl => OpCode.Inc,
                        OpCode.Jnz => OpCode.Cpy,
                        OpCode.Cpy => OpCode.Jnz,
                        _ => OpCode.Nop
                    };

                    break;
                }

                case OpCode.Mul:
                    if (instruction.C.IsRegister)
                    {
                        registers[instruction.C.Value] =
                            GetValue(instruction.A, registers) * GetValue(instruction.B, registers);
                    }

                    break;

                case OpCode.Out:
                {
                    var value = GetValue(instruction.A, registers);

                    output.Append(value);

                    switch (output.Length)
                    {
                        case 1 when output[0] == '1':
                        case > 1 when output[^1] == output[^2]:
                            return -1;
                        case > 100:
                            return 1;
                    }

                    break;
                }
            }

            programCounter++;
        }

        return registers[0];
    }

    private static Instruction[] ParseProgram(string[] input)
    {
        var program = new Instruction[input.Length];

        for (var i = 0; i < input.Length; i++)
        {
            var parts = input[i].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            program[i] = parts[0] switch
            {
                "cpy" => new Instruction(OpCode.Cpy, ParseOperand(parts[1]), ParseOperand(parts[2])),
                "inc" => new Instruction(OpCode.Inc, ParseOperand(parts[1])),
                "dec" => new Instruction(OpCode.Dec, ParseOperand(parts[1])),
                "jnz" => new Instruction(OpCode.Jnz, ParseOperand(parts[1]), ParseOperand(parts[2])),
                "tgl" => new Instruction(OpCode.Tgl, ParseOperand(parts[1])),
                "mul" => new Instruction(OpCode.Mul, ParseOperand(parts[1]), ParseOperand(parts[2]), ParseOperand(parts[3])),
                "out" => new Instruction(OpCode.Out, ParseOperand(parts[1])),
                _ => new Instruction(OpCode.Nop)
            };
        }

        return program;
    }

    private static Operand ParseOperand(string value)
    {
        return char.IsLetter(value[0])
            ? new Operand(value[0] - 'a', true)
            : new Operand(int.Parse(value), false);
    }

    private static int GetValue(Operand operand, ReadOnlySpan<int> registers)
    {
        return operand.IsRegister
            ? registers[operand.Value]
            : operand.Value;
    }

    private enum OpCode
    {
        Nop,
        Cpy,
        Inc,
        Dec,
        Jnz,
        Tgl,
        Mul,
        Out
    }

    private readonly record struct Operand(int Value, bool IsRegister);

    private struct Instruction(
        OpCode opCode,
        Operand a = default,
        Operand b = default,
        Operand c = default)
    {
        public OpCode OpCode = opCode;

        public Operand A = a;

        public Operand B = b;

        public Operand C = c;
    }
}
