namespace AoC.Solutions.Solutions._2021._22;

public class Instruction
{
    public bool State { get; }

    public Cuboid Cuboid { get; }

    public Instruction(bool state, Cuboid cuboid)
    {
        State = state;
        Cuboid = cuboid;
    }
}