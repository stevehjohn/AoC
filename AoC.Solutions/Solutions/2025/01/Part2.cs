using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._01;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var position = 50;

        var password = 0;

        foreach (var line in Input)
        {
            var clicks = int.Parse(line[1..]);

            var left = line[0] == 'L';
            
            while (clicks > 0)
            {
                position += left ? -1 : 1;

                if (position < 0)
                {
                    position += 100;
                }

                if (position > 99)
                {
                    position -= 100;
                }

                clicks--;

                if (position == 0)
                {
                    password++;
                }
            }
        }

        return password.ToString();
    }
}