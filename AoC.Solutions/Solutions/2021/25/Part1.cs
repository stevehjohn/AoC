using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._25;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var width = Input[0].Length;
        var height = Input.Length;

        var size = width * height;

        var cells = new char[size];

        var east = new int[size];
        var south = new int[size];

        var moves = new int[size];

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var index = y * width + x;

                cells[index] = Input[y][x];

                east[index] = y * width + (x + 1) % width;

                south[index] = (y + 1) % height * width + x;
            }
        }

        var steps = 0;

        while (true)
        {
            steps++;

            var eastCount = Move('>', east);

            var southCount = Move('v', south);

            if (eastCount + southCount == 0)
            {
                return steps.ToString();
            }
        }

        int Move(char cucumber, int[] destinations)
        {
            var count = 0;

            for (var i = 0; i < size; i++)
            {
                if (cells[i] == cucumber &&
                    cells[destinations[i]] == '.')
                {
                    moves[count++] = i;
                }
            }

            for (var i = 0; i < count; i++)
            {
                var source = moves[i];

                cells[source] = '.';

                cells[destinations[source]] = cucumber;
            }

            return count;
        }
    }
}