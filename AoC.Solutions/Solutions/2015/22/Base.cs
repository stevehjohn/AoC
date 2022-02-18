using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2015._22;

public abstract class Base : Solution
{
    public override string Description => "Wizard simulator 2015";

    private Player[] _players;

    private (int Cost, string Name, int Turns)[] _spells;

    private readonly List<(string Spell, int Turns)> _activeSpells = new();

    private readonly Random _random = new();

    protected (int Winner, int ManaCost) ExecuteFight()
    {
        var player = 0;

        var cost = 0;

        while (_players.All(p => p.HitPoints > 0))
        {
            _players[0].Armour = 0;

            foreach (var spell in _activeSpells)
            {
                switch (spell.Spell)
                {
                    case "MagicMissile":
                        _players[1].HitPoints = Math.Max(_players[1].HitPoints - 4, 0);

                        break;
                    case "Drain":
                        _players[1].HitPoints = Math.Max(_players[1].HitPoints - 2, 0);

                        _players[0].HitPoints += 2;

                        break;
                    case "Shield":
                        _players[0].Armour = 7;

                        break;
                    case "Poison":
                        _players[1].HitPoints = Math.Max(_players[1].HitPoints - 3, 0);

                        break;
                    case "Recharge":
                        _players[0].Mana += 101;

                        break;
                }
            }

            _activeSpells.ForEach(s => s.Turns--);

            _activeSpells.RemoveAll(s => s.Turns <= 0);

            if (player == 0)
            {
                cost += CastSpell();
            }
            else
            {
                var damage = Math.Max(_players[1].Damage - _players[0].Armour, 1);

                _players[0].HitPoints = Math.Max(_players[0].HitPoints - damage, 0);
            }

            player = 1 - player;
        }

        return (_players[0].HitPoints == 0 ? 1 : 0, cost);
    }

    private int CastSpell()
    {
        var canAfford = _spells.Where(s => s.Cost <= _players[0].Mana).ToList();

        if (canAfford.Count == 0)
        {
            return 0;
        }
        
        // TODO: Don't like this approach. 
        var spell = canAfford[_random.Next(canAfford.Count)];

        _activeSpells.Add((spell.Name, spell.Turns));

        return spell.Cost;
    }

    protected void InitialiseSpells()
    {
        _spells = new[] { (0, "Nothing", 0), (53, "MagicMissile", 1), (73, "Drain", 1), (113, "Shield", 6), (173, "Poison", 6), (229, "Recharge", 5) };
    }

    protected void InitialisePlayers()
    {
        _players = new Player[2];

        _players[0] = new Player
                      {
                          HitPoints = 50,
                          Damage = 0,
                          Armour = 0,
                          Mana = 500
                      };

        var inputData = Input.Select(l => l.Split(':', StringSplitOptions.TrimEntries)[1]).Select(int.Parse).ToArray();

        _players[1] = new Player
                      {
                          HitPoints = inputData[0],
                          Damage = inputData[1],
                          Armour = 0,
                          Mana = 0
                      };

        _activeSpells.Clear();
    }
}