using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._01;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var position = 50;

        var password = 0;

        foreach (var line in Input)
        {
            var clicks = int.Parse(line[1..]);

            if (line[0] == 'L')
            {
                clicks = -clicks;
            }

            position = (position + clicks + 100) % 100;

            if (position == 0)
            {
                password++;
            }
        }

        return password.ToString();
    }
}