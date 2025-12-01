using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._01;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var password = ProcessDocument();

        return password.ToString();
    }

    protected override int RotateDial(ref int position, bool left, int clicks)
    {
        var previousPosition = position;

        var zeroPasses = clicks / 100;

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
            zeroPasses++;
        }

        position = (position + 100) % 100;

        return zeroPasses;
    }
}