using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2015._22;

public abstract class Base : Solution
{
    public override string Description => "Wizard simulator 2015";

    protected int GetManaCostToWin(bool isPart2 = false)
    {
        var bossHealth =int.FastParse(Input[0].AsSpan()[(Input[0].IndexOf(':') + 1)..].Trim());

        var bossDamage =int.FastParse(Input[1].AsSpan()[(Input[1].IndexOf(':') + 1)..].Trim());

        var queue = new PriorityQueue<State, int>();
        var best = new Dictionary<State, int>();

        var initial = new State(
            Hp: 50,
            Mana: 500,
            BossHp: bossHealth,
            Shield: 0,
            Poison: 0,
            Recharge: 0,
            PlayerTurn: true);

        Enqueue(initial, 0);

        while (queue.TryDequeue(out var state, out var spent))
        {
            if (best[state] != spent)
            {
                continue;
            }

            if (state.BossHp <= 0)
            {
                return spent;
            }

            var hp = state.Hp;
            
            var mana = state.Mana;
            
            var bossHp = state.BossHp;

            var shield = state.Shield;
            
            var poison = state.Poison;
            
            var recharge = state.Recharge;

            if (state.PlayerTurn && isPart2)
            {
                if (--hp <= 0)
                {
                    continue;
                }
            }

            var armour = shield > 0 ? 7 : 0;

            if (shield > 0)
            {
                shield--;
            }

            if (poison > 0)
            {
                bossHp -= 3;
                
                poison--;
            }

            if (recharge > 0)
            {
                mana += 101;
                
                recharge--;
            }

            if (bossHp <= 0)
            {
                return spent;
            }

            if (! state.PlayerTurn)
            {
                hp -= Math.Max(1, bossDamage - armour);

                if (hp > 0)
                {
                    Enqueue(new State(hp, mana, bossHp, shield, poison, recharge, true), spent);
                }

                continue;
            }

            if (mana >= 53)
            {
                Enqueue(new State(hp, mana - 53, bossHp - 4, shield, poison, recharge, false), spent + 53);
            }

            if (mana >= 73)
            {
                Enqueue(new State(hp + 2, mana - 73, bossHp - 2, shield, poison, recharge, false), spent + 73);
            }

            if (mana >= 113 && shield == 0)
            {
                Enqueue(new State(hp, mana - 113, bossHp, 6, poison, recharge, false), spent + 113);
            }

            if (mana >= 173 && poison == 0)
            {
                Enqueue(new State(hp, mana - 173, bossHp, shield, 6, recharge, false), spent + 173);
            }

            if (mana >= 229 && recharge == 0)
            {
                Enqueue(new State(hp, mana - 229, bossHp, shield, poison, 5, false), spent + 229);
            }
        }

        return int.MaxValue;

        void Enqueue(State next, int cost)
        {
            if (best.TryGetValue(next, out var previous) && previous <= cost)
            {
                return;
            }

            best[next] = cost;
            
            queue.Enqueue(next, cost);
        }
    }
}