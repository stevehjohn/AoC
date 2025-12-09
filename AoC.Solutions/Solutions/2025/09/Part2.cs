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

                for (var i = 0; i < _lines.Length; i++)
                {
                    var line = _lines[i];

                    if (Intersects(left, right, line))
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

    private static bool Intersects(Coordinate rectangleStart, Coordinate rectangleEnd, Line line)
    {
        var minX = Math.Min(rectangleStart.X, rectangleEnd.X);
        
        var maxX = Math.Max(rectangleStart.X, rectangleEnd.X);
        
        var minY = Math.Min(rectangleStart.Y, rectangleEnd.Y);
        
        var maxY = Math.Max(rectangleStart.Y, rectangleEnd.Y);

        var p1 = line.Start;
        
        var p2 = line.End;

        if (p1.X == p2.X)
        {
            var x = p1.X;

            if (x <= minX || x >= maxX)
            {
                return false;
            }

            var y1 = Math.Min(p1.Y, p2.Y);

            var y2 = Math.Max(p1.Y, p2.Y);

            if (y2 <= minY || y1 >= maxY)
            {
                return false;
            }

            return true;
        }

        if (p1.Y == p2.Y)
        {
            var y = p1.Y;

            if (y <= minY || y >= maxY)
            {
                return false;
            }

            var x1 = Math.Min(p1.X, p2.X);
            
            var x2 = Math.Max(p1.X, p2.X);

            if (x2 <= minX || x1 >= maxX)
            {
                return false;
            }
        }

        return true;
    }
}