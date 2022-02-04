using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2018._24;

public abstract class Base : Solution
{
    public override string Description => "Immune system battle";

    private readonly List<Group> _immuneSystem = new();

    private readonly List<Group> _infection = new();

    protected void ParseInput()
    {
        var i = 1;

        var isInfection = false;

        while (i < Input.Length)
        {
            if (string.IsNullOrWhiteSpace(Input[i]))
            {
                i += 2;

                isInfection = true;
            }

            if (isInfection)
            {
                _infection.Add(new Group(Input[i]));
            }
            else
            {
                _immuneSystem.Add(new Group(Input[i]));
            }

            i++;
        }
    }
}