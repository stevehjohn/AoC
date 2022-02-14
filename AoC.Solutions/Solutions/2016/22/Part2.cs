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

        var viableNodes = GetViableNodes();

        var wallStart = nodes.Where(n => ! viableNodes.Contains(n)).Min(w => w.X);

        var moves = 0;

        var width = nodes.Max(n => n.X);

        // Move to left of wall
        moves += empty.X - wallStart + 1;

        empty.X -= moves;

        // Move to top
        moves += empty.Y;

        // Move to left of target
        moves += width - empty.X - 1;

        // Move target left, it takes five moves to move the target and then bring the empty tile back to the left of the target
        moves += (width - 1) * 5;

        // Final move
        moves++;

        return moves.ToString();
    }
}