namespace AoC.Solutions.Solutions._2020._16;

public class Rule
{
    public string Name { get; }

    public Range Rule1 { get; }

    public Range Rule2 { get; }

    public Rule(string name, Range rule1, Range rule2)
    {
        Name = name;

        Rule1 = rule1;

        Rule2 = rule2;
    }
}