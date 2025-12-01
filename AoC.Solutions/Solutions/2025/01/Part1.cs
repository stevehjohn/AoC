using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._01;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var password = ProcessDocument();

        return password.ToString();
    }

    protected override int RotateDial(ref int position, bool left, int clicks)
    {
        if (left)
        {
            clicks = -clicks;
        }

        position = (position + clicks + 100) % 100;

        return position == 0 ? 1 : 0;
    }
}