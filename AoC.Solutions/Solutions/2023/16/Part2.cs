using System.Collections.Concurrent;
using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._16;

[UsedImplicitly]
public class Part2 : Base
{
    public Part2()
    {
    }

    public Part2(IVisualiser<PuzzleState> visualiser) : base(visualiser)
    {
    }

    public override string GetAnswer()
    {
        if (Visualiser == null)
        {
            return GetAnswerParallel();
        }

        return GetAnswerConcurrent();
    }

    private string GetAnswerParallel()
    {
        ParseInput();

        var maxes = new ConcurrentBag<int>();

        Parallel.For(0, Width, x =>
        {
            var result = SimulateBeams(x, -1, 'S');

            var energised = CountEnergised(result);

            maxes.Add(energised);

            result = SimulateBeams(x, Height, 'N');

            energised = CountEnergised(result);

            maxes.Add(energised);
        });

        Parallel.For(0, Height, y =>
        {
            var result = SimulateBeams(Width, y, 'W');

            var energised = CountEnergised(result);

            maxes.Add(energised);

            result = SimulateBeams(-1, y, 'E');

            energised = CountEnergised(result);

            maxes.Add(energised);
        });
        
        return maxes.Max().ToString();
    }

    private string GetAnswerConcurrent()
    {
        ParseInput();

        var max = 0;

        int energised;
        
        for (var x = 0; x < Width; x++)
        {
            var result = SimulateBeams(x, -1, 'S');
        
            energised = CountEnergised(result);
        
            if (energised > max)
            {
                max = energised;
            }
        }
        
        for (var y = 0; y < Height; y++)
        {
            var result =SimulateBeams(Width, y, 'W');
        
            energised = CountEnergised(result);
        
            if (energised > max)
            {
                max = energised;
            }
        }
        
        for (var x = Width - 1; x >= 0; x--)
        {
            var result =SimulateBeams(x, Height, 'N');
        
            energised = CountEnergised(result);
        
            if (energised > max)
            {
                max = energised;
            }
        }
        
        for (var y = Height - 1; y >= 0; y--)
        {
            var result =SimulateBeams(-1, y, 'E');
        
            energised = CountEnergised(result);
        
            if (energised > max)
            {
                max = energised;
            }
        }
        
        return max.ToString();
    }
}