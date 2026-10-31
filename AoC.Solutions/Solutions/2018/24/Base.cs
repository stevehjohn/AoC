
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2018._24;

public abstract class Base : Solution
{
    public override string Description => "Immune system battle";

    private List<Group> _groups;

    protected (int Units, Type Side) Play(int immuneBoost = 0)
    {
        var groups = _groups.ToArray();
        
        var count = groups.Length;

        foreach (var group in groups)
        {
            if (group.Type == Type.ImmuneSystem)
            {
                group.DamagePoints += immuneBoost;
            }
        }

        var selectionOrder = new int[count];
        
        var attackOrder = new int[count];
        
        var targets = new int[count];
        
        var selected = new bool[count];

        for (var i = 0; i < count; i++)
        {
            selectionOrder[i] = i;
            
            attackOrder[i] = i;
        }

        Array.Sort(attackOrder, (a, b) => groups[b].Initiative.CompareTo(groups[a].Initiative));

        while (true)
        {
            for (var i = 0; i < count; i++)
            {
                selectionOrder[i] = i;
            }

            for (var i = 1; i < count; i++)
            {
                var index = selectionOrder[i];
                
                var group = groups[index];
                
                var j = i - 1;

                while (j >= 0)
                {
                    var other = groups[selectionOrder[j]];

                    if (other.EffectivePower > group.EffectivePower || other.EffectivePower == group.EffectivePower && other.Initiative >= group.Initiative)
                    {
                        break;
                    }

                    selectionOrder[j + 1] = selectionOrder[j];
                    
                    j--;
                }

                selectionOrder[j + 1] = index;
            }

            Array.Fill(targets, -1);
            
            Array.Clear(selected);

            foreach (var attackerIndex in selectionOrder)
            {
                var attacker = groups[attackerIndex];

                if (attacker.Units <= 0)
                {
                    continue;
                }

                var best = -1;
                
                var bestDamage = 0L;
                
                var bestPower = 0;
                
                var bestInitiative = 0;

                for (var i = 0; i < count; i++)
                {
                    var defender = groups[i];

                    if (selected[i] || defender.Units <= 0 || defender.Type == attacker.Type || defender.ImmuneTo.Contains(attacker.DamageType))
                    {
                        continue;
                    }

                    long damage = attacker.EffectivePower;

                    if (defender.WeakTo.Contains(attacker.DamageType))
                    {
                        damage *= 2;
                    }

                    if (damage > bestDamage || damage == bestDamage && (defender.EffectivePower > bestPower || defender.EffectivePower == bestPower && defender.Initiative > bestInitiative))
                    {
                        best = i;
                        
                        bestDamage = damage;
                        
                        bestPower = defender.EffectivePower;
                        
                        bestInitiative = defender.Initiative;
                    }
                }

                if (best >= 0)
                {
                    targets[attackerIndex] = best;
                    
                    selected[best] = true;
                }
            }

            var totalKills = 0;

            foreach (var attackerIndex in attackOrder)
            {
                var targetIndex = targets[attackerIndex];

                if (targetIndex < 0)
                {
                    continue;
                }

                var attacker = groups[attackerIndex];
                
                var defender = groups[targetIndex];

                if (attacker.Units <= 0 || defender.Units <= 0)
                {
                    continue;
                }

                long damage = attacker.EffectivePower;

                if (defender.WeakTo.Contains(attacker.DamageType))
                {
                    damage *= 2;
                }

                var kills = (int)Math.Min(damage / defender.HitPoints, defender.Units);

                defender.Units -= kills;
                
                totalKills += kills;
            }

            var immuneUnits = 0;
            
            var infectionUnits = 0;

            foreach (var group in groups)
            {
                if (group.Type == Type.ImmuneSystem)
                {
                    immuneUnits += group.Units;
                }
                else
                {
                    infectionUnits += group.Units;
                }
            }

            if (immuneUnits == 0 || infectionUnits == 0 || totalKills == 0)
            {
                _groups.RemoveAll(g => g.Units <= 0);

                if (totalKills == 0)
                {
                    return (immuneUnits + infectionUnits, Type.Neither);
                }

                return immuneUnits > 0
                    ? (immuneUnits, Type.ImmuneSystem)
                    : (infectionUnits, Type.Infection);
            }
        }
    }

    protected void ParseInput()
    {
        _groups = [];

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
