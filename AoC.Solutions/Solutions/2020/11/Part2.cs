
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._11;

[UsedImplicitly]
public class Part2 : Base
{
    private static readonly (int X, int Y)[] Directions =
    [
        (-1, -1), (0, -1), (1, -1),
        (-1,  0),          (1,  0),
        (-1,  1), (0,  1), (1,  1)
    ];

    public override string GetAnswer()
    {
        var width = Input[0].Length;
        
        var height = Input.Length;

        var indices = new int[width * height];
        
        Array.Fill(indices, -1);

        var positions = new List<int>();
        
        var initial = new List<byte>();

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var c = Input[y][x];

                if (c == '.')
                {
                    continue;
                }

                var position = y * width + x;

                indices[position] = positions.Count;
                
                positions.Add(position);
                
                initial.Add((byte)(c == '#' ? 1 : 0));
            }
        }

        var count = positions.Count;

        var neighbors = new int[count][];

        for (var seat = 0; seat < count; seat++)
        {
            var position = positions[seat];
            
            var x = position % width;
            
            var y = position / width;

            var visible = new List<int>(8);

            foreach (var (dx, dy) in Directions)
            {
                var nx = x + dx;
                
                var ny = y + dy;

                while (nx >= 0 && nx < width && ny >= 0 && ny < height)
                {
                    var index = indices[ny * width + nx];

                    if (index >= 0)
                    {
                        visible.Add(index);
                        
                        break;
                    }

                    nx += dx;
                    
                    ny += dy;
                }
            }

            neighbors[seat] = [.. visible];
        }

        var current = initial.ToArray();
        
        var next = new byte[count];

        while (true)
        {
            var changed = false;

            for (var seat = 0; seat < count; seat++)
            {
                var occupied = 0;
                
                var visible = neighbors[seat];

                for (var i = 0; i < visible.Length; i++)
                {
                    occupied += current[visible[i]];
                }

                var state = current[seat];

                state = state switch
                {
                    0 when occupied == 0 => 1,
                    1 when occupied >= 5 => 0,
                    _ => state
                };

                next[seat] = state;
                
                changed |= state != current[seat];
            }

            if (!changed)
            {
                break;
            }

            (current, next) = (next, current);
        }

        var result = 0;

        foreach (var seat in current)
        {
            result += seat;
        }

        return result.ToString();
    }
}
