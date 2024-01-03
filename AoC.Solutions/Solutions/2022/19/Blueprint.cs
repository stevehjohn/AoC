namespace AoC.Solutions.Solutions._2022._19;

public class Blueprint
{
    public int Id { get; }

    public Materials OreCost { get; }

    public Materials ClayCost { get; }

    public Materials ObsidianCost { get; }

    public Materials GeodeCost { get; }

    public int MaxOreCost { get; }

    public int MaxClayCost { get; }

    public int MaxObsidianCost { get; }

    public Blueprint(int id, Materials oreCost, Materials clayCost, Materials obsidianCost, Materials geodeCost)
    {
        Id = id;

        OreCost = oreCost;

        ClayCost = clayCost;

        ObsidianCost = obsidianCost;

        GeodeCost = geodeCost;
        
        MaxOreCost = new[] { oreCost.Ore, clayCost.Ore, obsidianCost.Ore, geodeCost.Ore}.Max();
        
        MaxClayCost = new[] { oreCost.Clay, clayCost.Clay, obsidianCost.Clay, geodeCost.Clay}.Max();
        
        MaxObsidianCost = new[] { oreCost.Obsidian, clayCost.Obsidian, obsidianCost.Obsidian, geodeCost.Obsidian}.Max();
    }
}