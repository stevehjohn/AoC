namespace AoC.Solutions.Solutions._2022._16;

public class Valve
{
    public string Name { get; }

    public int FlowRate { get; }

    public int Designation { get; set;  }

    public List<Valve> DirectConnections { get; }

    public List<(Valve Valve, int Cost)> WorkingValves { get; }

    public Valve(string name, int flowRate)
    {
        Name = name;

        FlowRate = flowRate;

        DirectConnections = [];

        WorkingValves = [];
    }
}