namespace AoC.Solutions.Solutions._2019._14;

public class Matter
{
    public string Name { get; set; }

    public int Amount { get; set; }

    public List<Matter> Components { get; }

    public Matter()
    {
        Components = new List<Matter>();
    }
}