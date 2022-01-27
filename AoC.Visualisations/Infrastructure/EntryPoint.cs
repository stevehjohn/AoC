using AoC.Visualisations.Visualisations._2018._13;

namespace AoC.Visualisations.Infrastructure;

public static class EntryPoint
{
    [STAThread]
    private static void Main()
    {
        using var visualisation = new Visualisation();
        
        visualisation.Run();
    }
}