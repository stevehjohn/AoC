using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2025._09;

public abstract class Base : Solution
{
    public override string Description => "Movie theater";

    protected Coordinate[] Coordinates;
    
    protected void ParseInput()
    {
        Coordinates = new Coordinate[Input.Length];

        var i = 0;
        
        foreach (var line in Input)
        {
            Coordinates[i++] = Coordinate.Parse(line);
        }
    }
}