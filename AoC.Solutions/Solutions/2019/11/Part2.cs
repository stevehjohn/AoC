using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._11;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        RunRobot(1);

        var minX = Panels.Min(p => p.Position.X);

        var width = Panels.Max(p => p.Position.X) - minX;

        var minY = Panels.Min(p => p.Position.Y);

        var height = Panels.Max(p => p.Position.Y) - minY;

        var image = new char[width + 1, height + 1];

        foreach (var panel in Panels)
        {
            image[panel.Position.X + Math.Abs(minX), panel.Position.Y + Math.Abs(minY)] = panel.Colour == 1 ? '*' : ' ';
        }

        for (var y = height; y >= 0; y--)
        {
            for (var x = 0; x <= width; x++)
            {
                Console.Write(image[x, y]);
            }

            Console.WriteLine();
        }

        return "TEST";
    }
}