using System.Collections.Immutable;
using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2024._16;

public struct State
{
    public Point Position { get; set; }
    
    public Point Direction { get; }
    
    public ImmutableStack<int> Path { get; set; }
    
    public int Score { get; set; }

    public State(Point position, Point direction, ImmutableStack<int> path, int score)
    {
        Position = position;
        
        Direction = direction;
        
        Path = path;
        
        Score = score;
    }
}