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

            var previousPosition = position;

            password += clicks / 100;

            if (left)
            {
                position -= clicks % 100;
            }
            else
            {
                position += clicks % 100;
            }

            if ((position < 1 && previousPosition != 0) || position > 99)
            {
                password++;
            }

            position = (position + 100) % 100;
        }

        return password.ToString();
    }
}