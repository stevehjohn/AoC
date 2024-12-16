using System.Collections.Immutable;
using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2024._16;

public struct State
{
    public int Position { get; set; }
    
    public int Dx { get; }
    
    public int Dy { get; }
    
    public ImmutableStack<int> Path { get; set; }
    
    public int Score { get; set; }

    public State(int position, int dX, int dY, ImmutableStack<int> path, int score)
    {
        Position = position;

        Dx = dX;

        Dy = dY;
        
        Path = path;
        
        Score = score;
    }
}