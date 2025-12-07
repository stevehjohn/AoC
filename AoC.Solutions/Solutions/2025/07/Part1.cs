using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._07;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly HashSet<int> _beams = [];

    public override string GetAnswer()
    {
        ParseInput();

        _beams.Add(Width / 2);

        var splits = 0;
        
        var y = 2;

        while (y < Height)
        {
            for (var x = 0; x < Width; x++)
            {
                if (Map[x, y] == '^' && _beams.Contains(x))
                {
                    _beams.Remove(x);

                    _beams.Add(x - 1);

                    _beams.Add(x + 1);

                    splits++;
                }
            }

            y += 2;
        }

        return splits.ToString();
    }
}