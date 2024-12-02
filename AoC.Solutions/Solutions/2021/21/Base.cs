using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2021._21;

public abstract class Base : Solution
{
    public override string Description => "Quantum dice";

    protected int Player1Position;

    protected int Player2Position;

    protected void ParseInput()
    {
        Player1Position = int.Parse(Input[0][(Input[0].IndexOf(':') + 1)..]);

        Player2Position = int.Parse(Input[1][(Input[1].IndexOf(':') + 1)..]);
    }
}