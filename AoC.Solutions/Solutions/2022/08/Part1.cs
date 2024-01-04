using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2022._08;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ProcessInput();

        var result = GetVisibleCount();

        return result.ToString();
    }

    private int GetVisibleCount()
    {
        var visible = Size * 4 - 4;

        for (var y = 1; y < Size - 1; y++)
        {
            for (var x = 1; x < Size - 1; x++)
            {
                var isVisible = IsVisible(x, y, -1, 0)
                                || IsVisible(x, y, 1, 0)
                                || IsVisible(x, y, 0, -1)
                                || IsVisible(x, y, 0, 1);

                if (isVisible)
                {
                    visible++;
                }
            }
        }

        return visible;
    }

    private bool IsVisible(int x, int y, int dx, int dy)
    {
        var tree = Matrix[x, y];

        do
        {
            x += dx;

            y += dy;

            if (Matrix[x, y] >= tree)
            {
                return false;
            }
        } while (x > 0 && x < Size - 1 && y > 0 && y < Size - 1);

        return true;
    }
}