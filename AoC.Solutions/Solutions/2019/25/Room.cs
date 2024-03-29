using System.Text;

namespace AoC.Solutions.Solutions._2019._25;

public class Room
{
    public string Name { get; init; }
    
    public Dictionary<string, int> VisitCount { get; init; }
    
    public Dictionary<string, Room> Directions { get; init; }

    public override string ToString()
    {
        var builder = new StringBuilder();
        
        foreach (var direction in Directions)
        {
            if (direction.Value != null)
            {
                builder.Append($"{direction.Key}: {direction.Value.Name} ");
            }
        }
        
        return $"{Name}. {builder}";
    }
}