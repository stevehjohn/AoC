namespace AoC.Solutions.Solutions._2016.Common;

public static partial class Cpu
{
    private struct Instruction(OpCode opCode, Operand a = default, Operand b = default, Operand c = default)
    {
        public OpCode OpCode = opCode;

        public readonly Operand A = a;

        public readonly Operand B = b;

        public readonly Operand C = c;
    }
}