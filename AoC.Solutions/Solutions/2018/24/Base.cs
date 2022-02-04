﻿using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2018._24;

public abstract class Base : Solution
{
    public override string Description => "Immune system battle";

    private readonly List<Group> _groups = new();

    protected void Play()
    {
        while (true)
        {
            foreach (var group in _groups)
            {
                Console.WriteLine($"{group.Type} {group.Id} contains {group.Units} units");
            }

            Console.WriteLine();

            var attacks = TargetSelection();

            Attack(attacks);

            if (_groups.DistinctBy(g => g.Type).Count() == 1)
            {
                break;
            }
        }

        foreach (var group in _groups)
        {
            Console.WriteLine($"{group.Type} {group.Id} contains {group.Units} units");
        }
    }

    private void Attack(List<(Group Attacker, Group Defender)> attacks)
    {
        var attackOrder = attacks.OrderByDescending(g => g.Attacker.Initiative);

        Console.WriteLine();

        foreach (var (attacker, target) in attackOrder)
        {
            var damage = attacker.EffectivePower * (target.WeakTo.Contains(attacker.DamageType) ? 2 : 1);

            var kills = Math.Min(damage / target.HitPoints, target.Units);

            target.Units -= kills;

            if (target.Units < 1)
            {
                _groups.Remove(target);
            }

            Console.WriteLine($"{attacker.Type} {attacker.Id} attacks defending group {target.Id} using {attacker.DamageType} killing {kills} units");
        }

        Console.WriteLine();
    }

    private List<(Group Attacker, Group Defender)> TargetSelection()
    {
        var selectionOrder = _groups.OrderByDescending(g => g.EffectivePower).ThenBy(g => g.Initiative);

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
                             .Where(g => ! g.ImmuneTo.Contains(attacker.DamageType))
                             .OrderBy(g => g.WeakTo.Contains(attacker.DamageType) ? 0 : 1)
                             .ThenByDescending(g => g.EffectivePower)
                             .ThenByDescending(g => g.Initiative);

        foreach (var target in targets)
        {
            var damage = attacker.EffectivePower * (target.WeakTo.Contains(attacker.DamageType) ? 2 : 1);

            Console.WriteLine($"{attacker.Type} group {attacker.Id} would deal defending group {target.Id} {damage} damage");
        }

        return targets.FirstOrDefault();
    }

    protected void ParseInput()
    {
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