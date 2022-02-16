namespace AoC.Solutions.Solutions._2015._14;

public class Reindeer
{
    public int Speed { get; set; }

    public int FlyTime { get; set; }
    
    public int RestTime { get; set; }
    
    public bool IsResting { get; set; }
    
    public int Ticks { get; set; }

    public int Distance { get; set; }

    public int Points { get; set; }

    public Reindeer(int speed, int flyTime, int restTime)
    {
        Speed = speed;

        FlyTime = flyTime;

        RestTime = restTime;

        IsResting = false;
    }
}