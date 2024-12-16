using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2024._16;

public struct State
{
    public Point Position { get; set; }
    
    public Point Direction { get; }
    
    public byte[] Path { get; set; }
    
    public int Score { get; set; }

    public State(Point position, Point direction, byte[] path, int score)
    {
        Position = position;
        
        Direction = direction;
        
        Path = path;
        
        Score = score;
    }
}