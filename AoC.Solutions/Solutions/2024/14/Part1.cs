using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._14;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();
        
        Simulate(100);

        var answer = CountArea(0, 0, Width / 2, Height / 2);

        answer *= CountArea(Width / 2 + 1, 0, Width / 2, Height / 2);

        answer *= CountArea(0, Height / 2 + 1, Width / 2, Height / 2);

        answer *= CountArea(Width / 2 + 1, Height / 2 + 1, Width / 2, Height / 2);
        
        return answer.ToString();
    }

    private int CountArea(int x, int y, int width, int height)
    {
        var count = 0;
        
        for (var i = 0; i < Robots.Length; i++)
        {
            var robot = Robots[i];

            if (robot.Position.X >= x && robot.Position.X < x + width && robot.Position.Y >= y && robot.Position.Y < y + height)
            {
                count++;
            }
        }

        return count;
    }
}