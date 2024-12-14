namespace AoC.Solutions.Solutions._2024._14;

public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var seconds = 0;

        while (true)
        {
            seconds++;
            
            Simulate(1);

            var unique = new HashSet<(int, int)>();
            
            for (var i = 0; i < Robots.Length; i++)
            {
                var robot = Robots[i];

                if (! unique.Add((robot.Position.X, robot.Position.Y)))
                {
                    break;
                }
            }

            if (unique.Count == Robots.Length)
            {
                break;
            }
        }

        return seconds.ToString();
    }
}