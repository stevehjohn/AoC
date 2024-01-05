namespace AoC.Solutions.Solutions._2019._25;

public class Room
{
    public string Name { get; set; }
    
    public string Item { get; set; }
    
    public Room North { get; set; }
    
    public Room East { get; set; }
    
    public Room South { get; set; }
    
    public Room West { get; set; }
}