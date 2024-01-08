namespace AoC.Games.Games.Deflectors.Levels;

public class Level
{
    public int Id { get; set; }
    
    public Start[] Starts { get; set; }

    public End[] Ends { get; set; }
    
    public Point[] Blocked { get; set; }

    public char[] Pieces { get; set; }
}