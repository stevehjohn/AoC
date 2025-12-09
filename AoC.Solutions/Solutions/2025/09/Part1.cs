using AoC.Solutions.Infrastructure;
using AoC.Solutions.Libraries;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._09;

[UsedImplicitly]
public class Part1 : Base
{
    private Coordinate[] _coordinates;
    
    public override string GetAnswer()
    {
        ParseInput();

        var largest = 0L;

        for (var l = 0; l < _coordinates.Length - 1; l++)
        {
            for (var r = l + 1; r < _coordinates.Length; r++)
            {
                var left = _coordinates[l];

                var right = _coordinates[r];

                var area = Measurement.AreaInCells(left, right);

                if (area > largest)
                {
                    largest = area;
                }
            }
        }

        return largest.ToString();
    }

    private void ParseInput()
    {
        _coordinates = new Coordinate[Input.Length];

        var i = 0;
        
        foreach (var line in Input)
        {
            _coordinates[i++] = Coordinate.Parse(line);
        }
    }
}