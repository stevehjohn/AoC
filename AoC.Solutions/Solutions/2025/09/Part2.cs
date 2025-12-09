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

        var length = Coordinates.Length;
        
        _lines = new Line[length];

        var previous = Coordinates[0];

        var index = 1;
        
        for (; index < length; index++)
        {
            var coordinate = Coordinates[index];
            
            _lines[index] = new Line(previous, coordinate);

            previous = coordinate;
        }

        _lines[0] = new Line(previous, Coordinates[0]);

        var largest = 0L;

        for (var l = 0; l < length - 1; l++)
        {
            for (var r = l + 1; r < length; r++)
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
        var (sX, sY) = line.Start;
        
        var (eX, eY) = line.End;


        if (sX == eX)
        {
            if (sX <= startX || sX >= endX)
            {
                return false;
            }

            var minY = Math.Min(sY, eY);

            var maxY = Math.Max(sY, eY);

            if (maxY <= startY || minY >= endY)
            {
                return false;
            }

            return true;
        }

        if (sY == eY)
        {
            if (sY <= startY || sY >= endY)
            {
                return false;
            }

            var minX = Math.Min(sX, eX);
            
            var maxX = Math.Max(sX, eX);

            if (maxX <= startX || minX >= endX)
            {
                return false;
            }
        }

        return true;
    }
}