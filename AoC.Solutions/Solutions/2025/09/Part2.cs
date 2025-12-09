using AoC.Solutions.Infrastructure;
using AoC.Solutions.Libraries;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._09;

[UsedImplicitly]
public class Part2 : Base
{
    private Line[] _lines;
    
    public override string GetAnswer()
    {
        ParseInput();

        _lines = new Line[Coordinates.Length];

        var previous = new Coordinate(-1, -1);

        var index = 0;
        
        foreach (var coordinate in Coordinates)
        {
            if (previous.X == -1)
            {
                previous = coordinate;
                
                continue;
            }

            _lines[index++] = new Line(previous, coordinate);

            previous = coordinate;
        }

        _lines[index] = new Line(previous, Coordinates[0]);

        var largest = 0L;

        for (var l = 0; l < Coordinates.Length - 1; l++)
        {
            for (var r = l + 1; r < Coordinates.Length; r++)
            {
                var left = Coordinates[l];

                var right = Coordinates[r];

                var area = Measurement.AreaInCells(left, right);

                if (area < largest)
                {
                    continue;
                }
                
                var empty = true;
                
                var startX = Math.Min(left.X, right.X);
        
                var endX = Math.Max(left.X, right.X);
        
                var startY = Math.Min(left.Y, right.Y);
        
                var endY = Math.Max(left.Y, right.Y);
                
                for (var i = 0; i < _lines.Length; i++)
                {
                    var line = _lines[i];

                    if (Intersects(startX, startY, endX, endY, line))
                    {
                        empty = false;
                        
                        break;
                    }
                }

                if (empty)
                {
                    largest = area;
                }
            }
        }

        return largest.ToString();
    }

    private static bool Intersects(long startX, long startY, long endX, long endY, Line line)
    {
        var start = line.Start;
        
        var end = line.End;

        if (start.X == end.X)
        {
            var x = start.X;
            
            if (x <= startX || x >= endX)
            {
                return false;
            }

            var minY = Math.Min(start.Y, end.Y);

            var maxY = Math.Max(start.Y, end.Y);

            if (maxY <= startY || minY >= endY)
            {
                return false;
            }

            return true;
        }

        if (start.Y == end.Y)
        {
            var y = start.Y;

            if (y <= startY || y >= endY)
            {
                return false;
            }

            var minX = Math.Min(start.X, end.X);
            
            var maxX = Math.Max(start.X, end.X);

            if (maxX <= startX || minX >= endX)
            {
                return false;
            }
        }

        return true;
    }
}