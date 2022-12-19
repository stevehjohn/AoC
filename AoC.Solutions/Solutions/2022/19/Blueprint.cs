namespace AoC.Solutions.Solutions._2022._19;

public class Blueprint
{
    public int Id { get; }

    public Materials OreCost { get; }

    public Materials ClayCost { get; }

    public Materials ObsidianCost { get; }

    public Materials GeodeCost { get; }

    public Blueprint(int id, Materials oreCost, Materials clayCost, Materials obsidianCost, Materials geodeCost)
    {
        Id = id;

        OreCost = oreCost;

        ClayCost = clayCost;

        ObsidianCost = obsidianCost;

        GeodeCost = geodeCost;
    }
}