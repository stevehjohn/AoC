using AoC.Solutions.Solutions._2017.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._14;

[UsedImplicitly]
public class Part2 : Base
{
    private readonly int[,] _disk = new int[128, 128];

    public override string GetAnswer()
    {
        BuildDiskImage();

        var result = IdentifyRegions();

        return result.ToString();
    }

    private int IdentifyRegions()
    {
        var id = 1;

        for (var y = 0; y < 128; y++)
        {
            for (var x = 0; x < 128; x++)
            {
                if (_disk[x, y] == -1)
                {
                    Flood(x, y, id);

                    id++;
                }
            }
        }

        return id - 1;
    }

    private void Flood(int x, int y, int id)
    {
        var queue = new Queue<(int X, int Y)>();

        queue.Enqueue((x, y));

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            _disk[current.X, current.Y] = id;

            if (current.X > 0 && _disk[current.X - 1, current.Y] == -1)
            {
                queue.Enqueue((current.X - 1, current.Y));
            }

            if (current.Y > 0 && _disk[current.X, current.Y - 1] == -1)
            {
                queue.Enqueue((current.X, current.Y - 1));
            }

            if (current.X < 127 && _disk[current.X + 1, current.Y] == -1)
            {
                queue.Enqueue((current.X + 1, current.Y));
            }

            if (current.Y < 127 && _disk[current.X, current.Y + 1] == -1)
            {
                queue.Enqueue((current.X, current.Y + 1));
            }
        }
    }

    private void BuildDiskImage()
    {
        for (var y = 0; y < 128; y++)
        {
            var rowHash = KnotHash.MakeHash($"{Input[0]}-{y}").ToList();

            var x = 0;

            foreach (var c in rowHash)
            {
                var b = "0123456789abcdef".IndexOf(c);

                var bit = 8;

                for (var i = 0; i < 4; i++)
                {
                    if ((b & bit) > 0)
                    {
                        _disk[x, y] = -1;
                    }

                    bit >>= 1;

                    x++;
                }
            }
        }
    }
}