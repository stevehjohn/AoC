using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._07;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var splits = 0;
        
        var y = 2;

        while (y < Height)
        {
            for (var x = 0; x < Width; x++)
            {
                if (Map[x, y] == '^' && Beams.Contains(x))
                {
                    Beams.Remove(x);

                    Beams.Add(x - 1);

                    Beams.Add(x + 1);

                    splits++;
                }
            }

            y += 2;
        }

        return splits.ToString();
    }
}