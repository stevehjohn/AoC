namespace AoC.Solutions.Solutions._2022._21;

public class Monkey
{
    public string Name { get; set; }

    public string Left { get; set; }

    public string Right { get; set; }

    public string Operator { get; set; }

    public long? Value { get; set; }

    public Monkey(string name, string left, string right, string @operator, long? value)
    {
        Name = name;

        Left = left;

        Right = right;

        Operator = @operator;

        Value = value;
    }
}