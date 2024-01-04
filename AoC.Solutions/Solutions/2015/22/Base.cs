using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2015._22;

public abstract class Base : Solution
{
    public override string Description => "Wizard simulator 2015";

    private (int Cost, string Name, int Turns)[] _spells;

    protected int GetManaCostToWin(bool isPart2 = false)
    {
        var queue = new PriorityQueue<(Player Player, Player Boss, Dictionary<string, int> ActiveSpells, int PlayerTurn, int TotalCost), int>();

        var inputData = Input.Select(l => l.Split(':', StringSplitOptions.TrimEntries)[1]).Select(int.Parse).ToArray();

        queue.Enqueue((new Player { HitPoints = 50, Mana = 500 }, new Player { HitPoints = inputData[0], Damage = inputData[1] }, new Dictionary<string, int>(), 0, 0), 0);

        while (queue.Count > 0)
        {
            var round = queue.Dequeue();

            var toRemove = new List<string>();

            round.Player.Armour = 0;

            if (isPart2 && round.PlayerTurn == 0)
            {
                round.Player.HitPoints--;

                if (round.Player.HitPoints <= 0)
                {
                    continue;
                }
            }

            foreach (var spell in round.ActiveSpells)
            {
                switch (spell.Key)
                {
                    case "Shield":
                        round.Player.Armour = 7;

                        break;
                    case "Poison":
                        round.Boss.HitPoints -= 3;

                        break;
                    case "Recharge":
                        round.Player.Mana += 101;

                        break;
                }

                round.ActiveSpells[spell.Key]--;

                if (round.ActiveSpells[spell.Key] <= 0)
                {
                    toRemove.Add(spell.Key);
                }
            }

            foreach (var item in toRemove)
            {
                round.ActiveSpells.Remove(item);
            }

            if (round.Boss.HitPoints <= 0)
            {
                return round.TotalCost;
            }

            if (round.PlayerTurn == 0)
            {
                var canCast = _spells.Where(s => ! round.ActiveSpells.ContainsKey(s.Name) && s.Cost <= round.Player.Mana).ToList();

                foreach (var spell in canCast)
                {
                    var player = new Player(round.Player);

                    player.Mana -= spell.Cost;

                    var activeSpells = round.ActiveSpells.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

                    var boss = new Player(round.Boss);

                    switch (spell.Name)
                    {
                        case "Shield":
                        case "Poison":
                        case "Recharge":
                            activeSpells.Add(spell.Name, spell.Turns);

                            queue.Enqueue((player, boss, activeSpells, 1, round.TotalCost + spell.Cost), round.TotalCost + spell.Cost);

                            break;
                        case "MagicMissile":
                            boss.HitPoints -= 4;

                            queue.Enqueue((player, boss, activeSpells, 1, round.TotalCost + spell.Cost), round.TotalCost + spell.Cost);
                            
                            break;
                        case "Drain":
                            boss.HitPoints -= 2;

                            player.HitPoints += 2;

                            queue.Enqueue((player, boss, activeSpells, 1, round.TotalCost + spell.Cost), round.TotalCost + spell.Cost);

                            break;
                    }

                }
            }
            else
            {
                round.Player.HitPoints -= Math.Max(round.Boss.Damage - round.Player.Armour, 1);
                
                if (round.Player.HitPoints > 0)
                {
                    queue.Enqueue((new Player(round.Player), new Player(round.Boss), round.ActiveSpells.ToDictionary(kvp => kvp.Key, kvp => kvp.Value), 0, round.TotalCost), round.TotalCost);
                }
            }
        }

        return int.MaxValue;
    }

    protected void InitialiseSpells()
    {
        _spells = [(53, "MagicMissile", 1), (73, "Drain", 1), (113, "Shield", 6), (173, "Poison", 6), (229, "Recharge", 5)];
    }
}