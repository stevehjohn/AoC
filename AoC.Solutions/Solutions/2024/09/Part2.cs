using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._09;

[UsedImplicitly]
public class Part2 : Base
{
    public Part2()
    {
    }

    public Part2(IVisualiser<PuzzleState> visualiser) : base(visualiser)
    {
    }

    public override string GetAnswer()
    {
        CalculateRequiredSize();

        IdentifyFiles();

        Visualise();

        var free = new List<(int Position, int Size)>();

        var position = 0;

        while (position < Size)
        {
            if (FileSystem[position] >= 0)
            {
                position++;

                continue;
            }

            var start = position;

            while (position < Size && FileSystem[position] == -1)
            {
                position++;
            }

            free.Add((start, position - start));
        }

        Span<int> cursors = stackalloc int[10];

        cursors.Clear();

        var map = Input[0];

        position = Size;

        for (var id = (map.Length - 1) / 2; id > 0; id--)
        {
            var index = id * 2;

            if (index + 1 < map.Length)
            {
                position -= map[index + 1] - '0';
            }

            var length = map[index] - '0';

            position -= length;

            if (length == 0)
            {
                continue;
            }

            Visualise();

            ref var cursor = ref cursors[length];

            while (cursor < free.Count && free[cursor].Size < length)
            {
                cursor++;
            }

            if (cursor == free.Count)
            {
                continue;
            }

            var block = free[cursor];

            if (block.Position >= position)
            {
                continue;
            }

            Array.Fill(FileSystem, id, block.Position, length);

            Array.Fill(FileSystem, -1, position, length);

            free[cursor] = (block.Position + length, block.Size - length);
        }

        return CalculateChecksum(false).ToString();
    }
}