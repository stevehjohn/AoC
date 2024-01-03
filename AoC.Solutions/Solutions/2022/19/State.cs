namespace AoC.Solutions.Solutions._2022._19;

public class State
{
    public int Ore { get; set; }

    public int Clay { get; set; }

    public int Obsidian { get; set; }

    public int Geodes { get; set; }

    public int OreBots { get; set; }

    public int ClayBots { get; set; }

    public int ObsidianBots { get; set; }

    public int GeodeBots { get; set; }

    public int ElapsedTime { get; set; }

    public State(int ore, int clay, int obsidian, int geodes, int oreBots, int clayBots, int obsidianBots, int geodeBots, int elapsedTime)
    {
        Ore = ore;

        Clay = clay;

        Obsidian = obsidian;

        Geodes = geodes;

        OreBots = oreBots;

        ClayBots = clayBots;

        ObsidianBots = obsidianBots;

        GeodeBots = geodeBots;

        ElapsedTime = elapsedTime;
    }

    public State(State state)
    {
        Ore = state.Ore;

        Clay = state.Clay;

        Obsidian = state.Obsidian;

        Geodes = state.Geodes;

        OreBots = state.OreBots;

        ClayBots = state.ClayBots;

        ObsidianBots = state.ObsidianBots;

        GeodeBots = state.GeodeBots;

        ElapsedTime = state.ElapsedTime;
    }

    public override string ToString()
    {
        return $"Time: {ElapsedTime, 2} Ore: {Ore, 3}, Clay: {Clay, 3}, Obsidian: {Obsidian, 3}, Geodes: {Geodes, 3}. OreBots: {OreBots, 3}, ClayBots: {ClayBots, 3}, ObsidianBots: {ObsidianBots, 3}, GeodeBots: {GeodeBots, 3}";
    }
}