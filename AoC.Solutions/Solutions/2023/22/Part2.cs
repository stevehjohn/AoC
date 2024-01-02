using System.Collections.Concurrent;
using System.Diagnostics;
using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._22;

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
        ParseInput();
        
        SettleBricks(Map);
        
        var result = GetSupportingBricks();

        var count = 0;
        
        Parallel.ForEach(result,
            () => 0,
            (brickId, _, c) =>
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
                            if (copy[z, x, y] == brickId)
                            {
                                for (var zD = 0; zD < 5; zD++)
                                {
                                    if (copy[z + zD, x, y] == brickId)
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

                return c + SettleBricks(copy, true, true);
            }, c => Interlocked.Add(ref count, c));

        if (Visualiser != null)
        {
            foreach (var id in result)
            {
                for (var z = 1; z < HighestZ; z++)
                {
                    for (var x = 0; x < 10; x++)
                    {
                        for (var y = 0; y < 10; y++)
                        {
                            if (Map[z, x, y] == id)
                            {
                                Map[z, x, y] = -1;
                            }
                        }
                    }
                }

            }
            
            SettleBricks(Map);
        }

        return count.ToString();
    }
    
    private List<int> GetSupportingBricks()
    {
        var result = new ConcurrentDictionary<int, bool>();

        Parallel.For(1, Count, id =>
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

            if (count > 0)
            {
                result.TryAdd(id, true);
            }

            if (count != 0)
            {
                Visualise(false, id);
            }
        });

        return result.Select(b => b.Key).ToList();
    }
}