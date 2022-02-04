using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2018._24;

public abstract class Base : Solution
{
    public override string Description => "Immune system battle";

    private List<Group> _groups;

    protected (int Units, Type Side) Play(int immuneBoost = 0)
    {
        foreach (var group in _groups.Where(g => g.Type == Type.ImmuneSystem))
        {
            group.DamagePoints += immuneBoost;
        }

        while (true)
        {
            var attacks = TargetSelection();

            if (! Attack(attacks))
            {
                return (_groups.Sum(g => g.Units), Type.Neither);
            }

            if (_groups.DistinctBy(g => g.Type).Count() == 1)
            {
                break;
            }
        }

        return (_groups.Sum(g => g.Units), _groups[0].Type);
    }

    private bool Attack(List<(Group Attacker, Group Defender)> attacks)
    {
        var attackOrder = attacks.OrderByDescending(g => g.Attacker.Initiative);

        var totalKills = 0;

        foreach (var (attacker, target) in attackOrder)
        {
            if (attacker.Units == 0)
            {
                continue;
            }

            var damage = attacker.EffectivePower * (target.WeakTo.Contains(attacker.DamageType) ? 2 : 1);

            var kills = Math.Min(damage / target.HitPoints, target.Units);

            totalKills += kills;

            target.Units -= kills;

            if (target.Units < 1)
            {
                _groups.Remove(target);
            }
        }

        if (totalKills == 0)
        {
            return false;
        }

        return true;
    }

    private List<(Group Attacker, Group Defender)> TargetSelection()
    {
        var selected = new List<Group>();

        var selectionOrder = _groups.OrderByDescending(g => g.EffectivePower).ThenByDescending(g => g.Initiative);

        var attacks = new List<(Group Attacker, Group Defender)>();

        foreach (var group in selectionOrder)
        {
            var victim = SelectVictim(group, selected);

            if (victim != null)
            {
                attacks.Add((group, victim));

                selected.Add(victim);
            }
        }

        return attacks;
    }

    private Group SelectVictim(Group attacker, List<Group> selected)
    {
        var target = _groups.Where(g => ! selected.Contains(g))
                            .Where(g => g.Type != attacker.Type)
                            .Where(g => ! g.ImmuneTo.Contains(attacker.DamageType))
                            .OrderBy(g => g.WeakTo.Contains(attacker.DamageType) ? 0 : 1)
                            .ThenByDescending(g => g.EffectivePower)
                            .ThenByDescending(g => g.Initiative)
                            .FirstOrDefault();

        return target;
    }

    protected void ParseInput()
    {
        _groups = new List<Group>();

        var i = 1;

        var isInfection = false;

        var id = 1;

        while (i < Input.Length)
        {
            if (string.IsNullOrWhiteSpace(Input[i]))
            {
                i += 2;

                isInfection = true;

                id = 1;
            }

            _groups.Add(isInfection
                            ? new Group(Input[i], Type.Infection) { Id = id }
                            : new Group(Input[i], Type.ImmuneSystem) { Id = id });

            i++;

            id++;
        }
    }
}