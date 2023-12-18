using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._18;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var (points, length) = ParseInput();

        var shiftedPoints = points.Skip(1).Append(points[0]);

        var zipped = points.Zip(shiftedPoints);

        var laces = zipped.Select(l => l.First.X * l.Second.Y - l.First.Y * l.Second.X);

        var area = Math.Abs(laces.Sum()) / 2;
        
        area += length / 2 + 1;
        
        return area.ToString();
    }

    private (List<(long X, long Y)> Points, long Length) ParseInput()
    {
        long x = 0, y = 0, totalLength = 1;

        var points = new List<(long X, long Y)>() { (0, 0) };

        foreach (var line in Input)
        {
            var parts = line.Split(' ');

            var length = Convert.ToInt32(parts[2][2..7], 16);

            totalLength += length;
            
            var direction = parts[2][7];

            switch (direction)
            {
                case '0':
                    x += length;
                    break;
                
                case '1':
                    y += length;
                    break;
                
                case '2':
                    x -= length;
                    break;
                
                case '3':
                    y -= length;
                    break;
                
            }
            
            points.Add((x, y));
        }
        
        return (points, totalLength);
    }
}