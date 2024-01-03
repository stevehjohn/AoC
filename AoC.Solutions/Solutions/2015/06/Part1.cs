using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._06;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var lights = new bool[1_000, 1_000];

        foreach (var line in Input)
        {
            var parts = line.Split(' ');

            string[] start;

            string[] end;

            if (parts[0] == "turn")
            {
                start = parts[2].Split(',');
             
                end = parts[4].Split(',');
            }
            else
            {
                start = parts[1].Split(',');
             
                end = parts[3].Split(',');
            }

            var startX = int.Parse(start[0]);
            
            var startY = int.Parse(start[1]);

            var endX = int.Parse(end[0]);
            
            var endY = int.Parse(end[1]);

            for (var y = startY; y <= endY; y++)
            {
                for (var x = startX; x <= endX; x++)
                {
                    if (parts[1] == "on")
                    {
                        lights[x, y] = true;
                    }
                    else if (parts[1] == "off")
                    {
                        lights[x, y] = false;
                    }
                    else
                    {
                        lights[x, y] = ! lights[x, y];
                    }
                }
            }
        }

        var on = 0;

        for (var y = 0; y < 1_000; y++)
        {
            for (var x = 0; x < 1_000; x++)
            {
                if (lights[x, y])
                {
                    on++;
                }
            }
        }

        return on.ToString();
    }
}