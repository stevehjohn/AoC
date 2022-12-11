namespace AoC.Solutions.Solutions._2022._11;

public class Monkey
{
    public List<long> Items { get; }

    public int DivisorTest { get; }

    public int PassTestMonkey { get; }

    public int FailTestMonkey { get; }

    public char Operator { get; }

    public int Operand { get; }

    public Monkey(int divisorTest, int passTestMonkey, int failTestMonkey, char @operator, int operand)
    {
        Items = new();

        DivisorTest = divisorTest;

        PassTestMonkey = passTestMonkey;

        FailTestMonkey = failTestMonkey;

        Operator = @operator;

        Operand = operand;
    }
}