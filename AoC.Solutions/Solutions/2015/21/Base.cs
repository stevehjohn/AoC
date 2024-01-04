using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2015._21;

public abstract class Base : Solution
{
    public override string Description => "RPG simulator 2015";

    private Player[] _players;

    private (int Cost, int Value)[] _weapons;
    
    private (int Cost, int Value)[] _armours;
    
    private (int Cost, int Value)[] _damageRings;
    
    private (int Cost, int Value)[] _defenceRings;

    private int _weapon;

    private int _armour;

    private int _damageRing;

    private int _defenceRing;

    protected int EquipPlayer()
    {
        _players[0].Damage = _weapons[_weapon].Value;
        
        _players[0].Armour = _armours[_armour].Value;

        _players[0].Damage += _damageRings[_damageRing].Value;
        
        _players[0].Armour += _defenceRings[_defenceRing].Value;

        var cost = _weapons[_weapon].Cost + _armours[_armour].Cost + _damageRings[_damageRing].Cost + _defenceRings[_defenceRing].Cost;

        _weapon++;

        if (_weapon >= _weapons.Length)
        {
            _weapon = 0;

            _armour++;

            if (_armour >= _armours.Length)
            {
                _armour = 0;

                _damageRing++;

                if (_damageRing >= _damageRings.Length)
                {
                    _damageRing = 0;

                    _defenceRing++;

                    if (_defenceRing >= _defenceRings.Length)
                    {
                        cost = int.MaxValue;
                    }
                }
            }
        }

        return cost;
    }

    protected void InitialiseStore()
    {
        _weapons = [(8, 4), (10, 5), (25, 6), (40, 7), (74, 8)];

        _armours = [(0, 0), (13, 1), (31, 2), (53, 3), (75, 4), (102, 5)];

        _damageRings = [(0, 0), (25, 1), (50, 2), (100, 3)];

        _defenceRings = [(0, 0), (20, 1), (40, 2), (80, 3)];
    }

    protected int ExecuteFight()
    {
        var player = 0;

        while (_players.All(p => p.HitPoints > 0))
        {
            var damage = Math.Max(_players[player].Damage - _players[1 - player].Armour, 1);

            _players[1 - player].HitPoints = Math.Max(_players[1 - player].HitPoints - damage, 0);

            player = 1 - player;
        }

        return _players[0].HitPoints == 0 ? 1 : 0;
    }

    protected void InitialisePlayers()
    {
        _players = new Player[2];

        _players[0] = new Player
                      {
                          HitPoints = 100,
                          Damage = 0,
                          Armour = 0
                      };

        var inputData = Input.Select(l => l.Split(':', StringSplitOptions.TrimEntries)[1]).Select(int.Parse).ToArray();

        _players[1] = new Player
                      {
                          HitPoints = inputData[0],
                          Damage = inputData[1],
                          Armour = inputData[2]
                      };
    }
}