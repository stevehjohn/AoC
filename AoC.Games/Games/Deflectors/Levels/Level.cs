using JetBrains.Annotations;

namespace AoC.Games.Games.Deflectors.Levels;

[UsedImplicitly]
public class Level
{
    public int Id { get; set; }
    
    public Start[] Starts { get; set; }

    public End[] Ends { get; set; }
    
    public Point[] Blocked { get; set; }

    public List<char> Pieces { get; set; }

    public List<Mirror> Mirrors { get; set; }
}