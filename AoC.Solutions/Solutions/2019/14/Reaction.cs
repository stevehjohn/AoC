namespace AoC.Solutions.Solutions._2019._14;

public class Reaction
{
    public Matter Result { get; set; }

    public List<Matter> Materials { get; }

    public Reaction()
    {
        Materials = new List<Matter>();
    }
}