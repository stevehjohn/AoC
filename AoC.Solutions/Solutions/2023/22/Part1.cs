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
                        // ReSharper disable once AccessToModifiedClosure
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
            
            result += 1 - count;

            if (count == 0)
            {
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