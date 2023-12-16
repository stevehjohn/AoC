namespace AoC.Visualisations.Visualisations._2019._18;

public class Willy
{
    public int MapX { get; set; }
    
    public int MapY { get; set; }
    
    public int Frame { get; set; }
    
    public int FrameDirection { get; set; }

    public bool Moving;
    
    public int Direction { get; set; }

    public char Cell { get; set; } = '\0';
}