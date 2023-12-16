using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._16;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var max = 0;

        for (var x = 0; x < Width; x++)
        {
            SimulateBeams(x, -1, 'S');

            var energised = CountEnergised();

            if (energised > max)
            {
                max = energised;
            }

            SimulateBeams(x, Height, 'N');

            energised = CountEnergised();

            if (energised > max)
            {
                max = energised;
            }
        }

        for (var y = 0; y < Width; y++)
        {
            SimulateBeams(-1, y, 'E');

            var energised = CountEnergised();

            if (energised > max)
            {
                max = energised;
            }

            SimulateBeams(Width, y, 'W');

            energised = CountEnergised();

            if (energised > max)
            {
                max = energised;
            }
        }
        
        return max.ToString();
    }
}