namespace AoC.Solutions.Solutions._2022._11;

public struct Monkey
{
    public FastList<ulong> Items { get; }

    public ulong DivisorTest { get; }

    public int PassTestMonkey { get; }

    public int FailTestMonkey { get; }

    public char Operator { get; }

    public ulong Operand { get; }

    public Monkey(ulong divisorTest, int passTestMonkey, int failTestMonkey, char @operator, ulong operand)
    {
        Items = new(32);

        DivisorTest = divisorTest;

        PassTestMonkey = passTestMonkey;

        FailTestMonkey = failTestMonkey;

        Operator = @operator;

        Operand = operand;
    }
}