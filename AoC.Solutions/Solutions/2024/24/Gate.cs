namespace AoC.Solutions.Solutions._2024._24;

public readonly struct Gate
{
    public string Left { get; }
    
    public string Right { get; }
    
    public Type Type { get; }

    public Gate(string left, string right, Type type)
    {
        Left = left;
        
        Right = right;
        
        Type = type;
    }

    public override string ToString()
    {
        return $"{Left} {Type} {Right}";
    }
}