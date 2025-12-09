using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2015._22;

public abstract class Base : Solution
{
    public override string Description => "Wizard simulator 2015";

    private readonly SpellData[] _spells =
    [
        new(Spell.MagicMissile, 53, 0),
        new(Spell.Drain, 73, 0),
        new(Spell.Shield, 113, 6),
        new(Spell.Poison, 173, 6),
        new(Spell.Recharge, 229, 5)
    ];

    private readonly int _effectCounts = Enum.GetValues<Spell>().Length;


    protected int GetManaCostToWin(bool isPart2 = false)
    {
        var queue = new PriorityQueue<(Player Player, Player Boss, int[] Active, int PlayerTurn, int TotalCost), int>();

        var bossHealth = int.Parse(Input[0].Split(':', StringSplitOptions.TrimEntries)[1]);

        var bossDamage = int.Parse(Input[1].Split(':', StringSplitOptions.TrimEntries)[1]);

        var player = new Player { HitPoints = 50, Mana = 500 };

        var boss = new Player { HitPoints = bossHealth, Damage = bossDamage };

        queue.Enqueue((player, boss, new int[_effectCounts], 0, 0), 0);

        while (queue.Count > 0)
        {
            (player, boss, var active, var playerTurn, var totalCost) = queue.Dequeue();

            if (isPart2 && playerTurn == 0)
            {
                player.HitPoints--;

                if (player.HitPoints <= 0)
                {
                    continue;
                }
            }

            ApplyEffects(player, boss, active);

            if (boss.HitPoints <= 0)
            {
                return totalCost;
            }

            if (playerTurn == 0)
            {
                for (var i = 0; i < _spells.Length; i++)
                {
                    var spell = _spells[i];

                    if (spell.Cost > player.Mana)
                    {
                        continue;
                    }

                    if (spell.Duration > 0 && active[(int) spell.Id] > 0)
                    {
                        continue;
                    }

                    var nextPlayer = new Player(player) { Mana = player.Mana - spell.Cost };

                    var nextBoss = new Player(boss);

                    var nextActive = (int[]) active.Clone();

                    var nextCost = totalCost + spell.Cost;

                    // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
                    switch (spell.Id)
                    {
                        case Spell.MagicMissile:
                            nextBoss.HitPoints -= 4;

                            break;

                        case Spell.Drain:
                            nextBoss.HitPoints -= 2;
                            nextPlayer.HitPoints += 2;

                            break;

                        case Spell.Shield:
                        case Spell.Poison:
                        case Spell.Recharge:
                            nextActive[(int) spell.Id] = spell.Duration;

                            break;
                    }

                    if (nextBoss.HitPoints <= 0)
                    {
                        return nextCost;
                    }

                    queue.Enqueue((nextPlayer, nextBoss, nextActive, 1, nextCost), nextCost);
                }
            }
            else
            {
                var damage = Math.Max(boss.Damage - player.Armour, 1);

                var nextPlayer = new Player(player) { HitPoints = player.HitPoints - damage };

                if (nextPlayer.HitPoints > 0)
                {
                    var nextBoss = new Player(boss);

                    queue.Enqueue((nextPlayer, nextBoss, (int[]) active.Clone(), 0, TotalCost: totalCost), totalCost);
                }
            }
        }

        return int.MaxValue;
    }

    private static void ApplyEffects(Player player, Player boss, int[] active)
    {
        player.Armour = 0;

        if (active[(int) Spell.Shield] > 0)
        {
            player.Armour = 7;

            active[(int) Spell.Shield]--;
        }

        if (active[(int) Spell.Poison] > 0)
        {
            boss.HitPoints -= 3;

            active[(int) Spell.Poison]--;
        }

        if (active[(int) Spell.Recharge] > 0)
        {
            player.Mana += 101;

            active[(int) Spell.Recharge]--;
        }
    }
}