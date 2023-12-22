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
        
        var result = GetSupportingBricks();

        var count = 0;

        var settled = new List<(int Id, List<Point> Points)>();

        foreach (var brick in Bricks)
        {
            settled.Add((brick.Id, brick.Points.Select(b => new Point(b)).ToList()));
        }

        foreach (var brickId in result)
        {
            Bricks.RemoveAll(b => b.Id == brickId);

            count += SettleBricks();
            
            Bricks.Clear();

            foreach (var item in settled)
            {
                Bricks.Add((item.Id, item.Points.Select(b => new Point(b)).ToList()));
            }
        }
        
        return count.ToString();
    }
    
    private List<int> GetSupportingBricks()
    {
        var result = new List<int>();

        var settledState = Bricks.ToList();

        foreach (var brick in settledState)
        {
            Bricks.Remove(brick);

            if (SettleBricks(false) > 0)
            {
                result.Add(brick.Id);                    
            }

            Bricks.Add(brick);
        }
        
        return result;
    }
}