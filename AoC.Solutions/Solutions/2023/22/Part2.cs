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

        var settled = new List<(int Id, List<Point> Points)>();

        foreach (var brick in Bricks)
        {
            settled.Add((brick.Id, brick.Points.Select(b => new Point(b)).ToList()));
        }

        foreach (var brickId in result)
        {
            Bricks.RemoveAll(b => b.Id == brickId);

            count += SettleBricks2();
            
            Bricks.Clear();

            foreach (var item in settled)
            {
                Bricks.Add((item.Id, item.Points.Select(b => new Point(b)).ToList()));
            }
        }
        
        return count.ToString();
    }
    
    private int SettleBricks2(bool move = true)
    {
        bool moved;

        var movedItems = new HashSet<int>();
        
        do
        {
            moved = false;
                
            foreach (var brick in Bricks)
            {
                if (brick.Points[0].Z == 1)
                {
                    continue;
                }

                if (! Resting(brick.Points))
                {
                    if (move)
                    {
                        foreach (var item in brick.Points)
                        {
                            item.Z--;
                        }

                        while (! Resting(brick.Points) && brick.Points[0].Z > 1)
                        {
                            foreach (var item in brick.Points)
                            {
                                item.Z--;
                            }
                        }
                    }

                    moved = true;

                    movedItems.Add(brick.Id);
                }
            }
        } while (moved);
        
        return movedItems.Count;
    }
    
    private List<int> CountSupportingBricks()
    {
        var result = new List<int>();

        var settledState = Bricks.ToList();

        foreach (var brick in settledState)
        {
            Bricks.Remove(brick);

            if (SettleBricks(false))
            {
                result.Add(brick.Id);                    
            }

            Bricks.Add(brick);
        }
        
        return result;
    }
}