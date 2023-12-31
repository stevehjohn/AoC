namespace AoC.Solutions.Solutions._2016._01;

public class Part1 : Base
{
    public override string GetAnswer()
    {
        var result = GetDistance();

        return result.ToString();
    }
    
    private int GetDistance()
    {
        var steps = Input[0].Split(", ", StringSplitOptions.TrimEntries);

        var x = 0;

        var y = 0;

        var dX = 0;

        var dY = -1;

        foreach (var step in steps)
        {
            if (step[0] == 'R')
            {
                (dX, dY) = (-dY, dX);
            }
            else
            {
                (dX, dY) = (dY, -dX);
            }

            var amount = int.Parse(step[1..]);

            x += dX * amount;

            y += dY * amount;
        }

        return Math.Abs(x) + Math.Abs(y);
    }
}