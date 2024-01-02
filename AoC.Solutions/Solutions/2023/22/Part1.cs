using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._22;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        SettleBricks(Map);

        var result = CountNonSupportingBricks();
        
        return result.ToString();
    }

    public Part1()
    {
    }

    public Part1(IVisualiser<PuzzleState> visualiser) : base(visualiser)
    {
    }

    private int CountNonSupportingBricks()
    {
        var result = 0;

        Parallel.For(1, Count + 1,
            () => 0,
            (id, _, c) =>
            {
                var copy = new int[MaxHeight, 10, 10];

                Array.Copy(Map, copy, MaxHeight * 100);

                var removed = false;

                for (var z = 1; z < HighestZ; z++)
                {
                    for (var x = 0; x < 10; x++)
                    {
                        for (var y = 0; y < 10; y++)
                        {
                            // ReSharper disable once AccessToModifiedClosure
                            if (copy[z, x, y] == id)
                            {
                                for (var zD = 0; zD < 5; zD++)
                                {
                                    if (copy[z + zD, x, y] == id)
                                    {
                                        copy[z + zD, x, y] = -1;
                                    }
                                }

                                removed = true;
                            }
                        }
                    }

                    if (removed)
                    {
                        break;
                    }
                }

                var count = SettleBricks(copy, false);

                if (count == 0)
                {
                    Visualise(false, id);
                }

                return c + (1 - count);
            }, c => Interlocked.Add(ref result, c));

        return result;
    }
}