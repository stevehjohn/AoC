namespace AoC.Solutions.Solutions._2024._16;

public readonly struct Edge
{
    private readonly List<Vertex> _vertices = [];

    public int Id { get; }
    
    public string MetaData { get; }
    
    public IReadOnlyList<Vertex> Vertices => _vertices;
    
    public Edge(int id, string metaData)
    {
        Id = id;

        MetaData = metaData;
    }

    public void AddHeading(Vertex vertex)
    {
        _vertices.Add(vertex);
    }
}