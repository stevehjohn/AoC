using AoC.Solutions.Common;
using AoC.Solutions.Exceptions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._22;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var data = Input.Skip(2).ToList();

        var nodes = new List<(int X, int Y, int Used, int Available)>();

        Point empty = null;

        foreach (var item in data)
        {
            var parsed = ParseLine(item);

            nodes.Add(parsed);

            if (parsed.Used == 0)
            {
                empty = new Point(parsed.X, parsed.Y);
            }
        }

        if (empty == null)
        {
            throw new PuzzleException("Solution not found.");
        }

        var viableNodes = GetViableNodes().ToHashSet();

        var wallStart = nodes.Where(n => !viableNodes.Contains(n)).Min(n => n.X);
        
        var moves = 0;

        var width = nodes.Max(n => n.X);

        moves += empty.X - wallStart + 1;

        empty.X -= moves;

        moves += empty.Y;

        moves += width - empty.X - 1;

        moves += (width - 1) * 5;

        moves++;

        return moves.ToString();
    }
}