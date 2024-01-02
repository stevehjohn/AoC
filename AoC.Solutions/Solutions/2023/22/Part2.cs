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
        var result = new List<int>();

        var replace = new HashSet<(int X, int Y, int Z)>();
        
        for (var id = 1; id <= Count; id++)
        {
            var removed = false;
                
            for (var z = 1; z < HighestZ; z++)
            {
                for (var x = 0; x < 10; x++)
                {
                    for (var y = 0; y < 10; y++)
                    {
                        if (Map[z, x, y] == id)
                        {
                            for (var zD = 0; zD < 5; zD++)
                            {
                                if (Map[z + zD, x, y] == id)
                                {
                                    Map[z + zD, x, y] = -1;
                                    
                                    replace.Add((x, y, z + zD));
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

            var count = SettleBricks(Map, false);
            
            if (count > 0)
            {
                result.Add(id);

                Visualise(false, id);
            }
            
            foreach (var brick in replace)
            {
                Map[brick.Z, brick.X, brick.Y] = id;
            }
            
            replace.Clear();
        }

        return result;
    }
}