using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2018._24;

public abstract class Base : Solution
{
    public override string Description => "Immune system battle";

    private readonly List<Group> _groups = new();

    protected void Play()
    {
        var attacks = TargetSelection();

        Attack(attacks);
    }

    private void Attack(List<(Group Attacker, Group Defender)> attacks)
    {
        var attackOrder = attacks.OrderByDescending(g => g.Attacker.Initiative);
    }

    private List<(Group Attacker, Group Defender)> TargetSelection()
    {
        var selectionOrder = _groups.OrderBy(g => g.EffectivePower).ThenBy(g => g.Initiative);

        var attacks = new List<(Group Attacker, Group Defender)>();

        foreach (var group in selectionOrder)
        {
            var victim = SelectVictim(group);

            if (victim != null)
            {
                attacks.Add((group, victim));
            }
        }

        return attacks;
    }

    private Group SelectVictim(Group attacker)
    {
        var targets = _groups.Where(g => g.Type != attacker.Type && ! g.ImmuneTo.Contains(attacker.DamageType))
                             .OrderBy(g => g.WeakTo.Contains(attacker.DamageType) ? 0 : 1)
                             .ThenBy(g => g.EffectivePower)
                             .ThenBy(g => g.Initiative);

        return targets.FirstOrDefault();
    }

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

            _groups.Add(isInfection
                            ? new Group(Input[i], Type.Infection)
                            : new Group(Input[i], Type.ImmuneSystem));

            i++;
        }
    }
}