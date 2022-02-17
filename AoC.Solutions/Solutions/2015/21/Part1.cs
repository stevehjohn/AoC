using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._21;

[UsedImplicitly]
public class Part1 : Base
{
    private Player[] _players;

    public override string GetAnswer()
    {
        InitialisePlayers();

        ExecuteFight();

        return "TESTING";
    }

    private void ExecuteFight()
    {
        var player = 0;

        while (_players.All(p => p.HitPoints > 0))
        {
            var damage = Math.Max(_players[player].Damage - _players[1 - player].Armor, 1);

            _players[1 - player].HitPoints = Math.Max(_players[1 - player].HitPoints - damage, 0);

            Console.WriteLine($"Player {player} deals {damage}, player {1 - player} has {_players[1 - player].HitPoints} hit points.");

            player = 1 - player;
        }
    }

    private void InitialisePlayers()
    {
        _players = new Player[2];

        _players[0] = new Player
                      {
                          HitPoints = 8, //100,
                          Damage = 5, //0,
                          Armor = 5 //0
                      };

        var inputData = Input.Select(l => l.Split(':', StringSplitOptions.TrimEntries)[1]).Select(int.Parse).ToArray();

        _players[1] = new Player
                      {
                          HitPoints = 12, //inputData[0],
                          Damage = 7, //inputData[1],
                          Armor = 2 //inputData[2]
                      };
    }
}