using AoC.Solutions.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._22;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();
        
        SettleBricks();
        
        var result = CountSupportingBricks();

        var count = 0;

        var settled = new List<List<Point>>();

        foreach (var brick in Bricks)
        {
            settled.Add(brick.Select(b => new Point(b)).ToList());
        }

        foreach (var brick in result)
        {
            Bricks.Remove(brick);

            count += SettleBricks2();
            
            Bricks.Clear();

            foreach (var item in settled)
            {
                Bricks.Add(item.Select(b => new Point(b)).ToList());
            }
        }
        
        return count.ToString();
    }
    
    private int SettleBricks2(bool move = true)
    {
        bool moved;

        var movedItems = new HashSet<List<Point>>();
        
        do
        {
            moved = false;
                
            foreach (var brick in Bricks)
            {
                if (brick[0].Z == 1)
                {
                    continue;
                }

                if (! Resting(brick))
                {
                    if (move)
                    {
                        foreach (var item in brick)
                        {
                            item.Z--;
                        }
                    }

                    moved = true;

                    movedItems.Add(brick);
                }
            }
        } while (moved);

        return movedItems.Count;
    }
    
    private List<List<Point>> CountSupportingBricks()
    {
        var result = new List<List<Point>>();

        var settledState = Bricks.ToList();

        foreach (var brick in settledState)
        {
            Bricks.Remove(brick);

            if (SettleBricks(false))
            {
                result.Add(brick);                    
            }

            Bricks.Add(brick);
        }
        
        return result;
    }
}