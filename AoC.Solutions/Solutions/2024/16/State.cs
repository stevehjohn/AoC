using System.Collections.Immutable;

namespace AoC.Solutions.Solutions._2024._16;

public struct State
{
    public int Position { get; }
    
    public int Dx { get; }
    
    public int Dy { get; }
    
    public int Score { get; }

    public State(int position, int dX, int dY, int score)
    {
        Position = position;

        Dx = dX;

        Dy = dY;
        
        Score = score;
    }
}