using JetBrains.Annotations;

namespace AoC.Games.Games.Deflectors.Levels;

[UsedImplicitly]
public class Mirror : Point
{
    public char Piece { get; set; }
    
    public bool Placed { get; set; }
}