using JetBrains.Annotations;

namespace AoC.Games.Games.Deflectors.Levels;

[UsedImplicitly]
public class Level
{
    [UsedImplicitly]
    public int Id { get; set; }
    
    [UsedImplicitly]
    public Start[] Starts { get; set; }

    [UsedImplicitly]
    public End[] Ends { get; set; }
    
    [UsedImplicitly]
    public Point[] Blocked { get; set; }

    [UsedImplicitly]
    public List<char> Pieces { get; set; }

    [UsedImplicitly]
    public List<Mirror> Mirrors { get; set; }
}