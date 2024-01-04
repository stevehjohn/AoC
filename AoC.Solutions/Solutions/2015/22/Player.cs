namespace AoC.Solutions.Solutions._2015._22;

public class Player
{
    public int HitPoints { get; set; }

    public int Damage { get; init; }

    public int Armour { get; set; }

    public int Mana { get; set; }

    public Player()
    {
    }

    public Player(Player player)
    {
        HitPoints = player.HitPoints;

        Damage = player.Damage;

        Armour = player.Armour;

        Mana = player.Mana;
    }
}