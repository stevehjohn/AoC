using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2021._20;

public abstract class Base : Solution
{
    public override string Description => "NCIS image enhancement";

    protected string GetAnswer(int iterations)
    {
        var algorithm = new byte[512];

        for (var i = 0; i < 512; i++)
        {
            algorithm[i] = (byte) (Input[0][i] == '#' ? 1 : 0);
        }

        var padding = iterations + 1;

        var width = Input[2].Length + padding * 2;

        var height = Input.Length - 2 + padding * 2;

        var current = new byte[width * height];

        var next = new byte[current.Length];

        for (var y = 2; y < Input.Length; y++)
        {
            var line = Input[y];

            var offset = (y - 2 + padding) * width + padding;

            for (var x = 0; x < line.Length; x++)
            {
                current[offset + x] = (byte) (line[x] == '#' ? 1 : 0);
            }
        }

        var infinity = (byte) 0;

        for (var iteration = 0; iteration < iterations; iteration++)
        {
            infinity = algorithm[infinity == 0 ? 0 : 511];

            next.AsSpan(0, width).Fill(infinity);

            next.AsSpan((height - 1) * width, width).Fill(infinity);

            for (var y = 1; y < height - 1; y++)
            {
                var middle = y * width;

                var top = middle - width;

                var bottom = middle + width;

                next[middle] = infinity;

                next[middle + width - 1] = infinity;

                var a = (current[top] << 1) | current[top + 1];

                var b = (current[middle] << 1) | current[middle + 1];

                var c = (current[bottom] << 1) | current[bottom + 1];

                for (var x = 1; x < width - 1; x++)
                {
                    a = ((a << 1) | current[top + x + 1]) & 7;

                    b = ((b << 1) | current[middle + x + 1]) & 7;

                    c = ((c << 1) | current[bottom + x + 1]) & 7;

                    next[middle + x] = algorithm[
                        (a << 6) | (b << 3) | c];
                }
            }

            (current, next) = (next, current);
        }

        if (infinity != 0)
        {
            throw new InvalidOperationException("Infinite lit pixels.");
        }

        var count = 0;

        foreach (var pixel in current)
        {
            count += pixel;
        }

        return count.ToString();
    }
}