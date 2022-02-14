using AoC.Solutions.Common;
using AoC.Solutions.Exceptions;
using JetBrains.Annotations;
using System.Security.Cryptography;
using System.Text;

namespace AoC.Solutions.Solutions._2016._17;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var result = Solve(Input[0]);

        return result;
    }

    private static string Solve(string passcode)
    {
        var queue = new PriorityQueue<(string Passcode, string Directions, Point Position, int Steps), int>();

        queue.Enqueue((passcode, string.Empty, new Point(0, 0), 0), 0);

        var open = new[] { 'B', 'C', 'D', 'E', 'F' };

        while (queue.Count > 0)
        {
            var item = queue.Dequeue();

            if (item.Position.X == 3 && item.Position.Y == 3)
            {
                return item.Directions;
            }

            var hash = Convert.ToHexString(MD5.HashData(Encoding.ASCII.GetBytes($"{item.Passcode}{item.Directions}")));

            if (open.Contains(hash[0]) && item.Position.Y > 0)
            {
                queue.Enqueue((item.Passcode, $"{item.Directions}U", new Point(item.Position.X, item.Position.Y - 1), item.Steps + 1), item.Steps + 1);
            }

            if (open.Contains(hash[1]) && item.Position.Y < 3)
            {
                queue.Enqueue((item.Passcode, $"{item.Directions}D", new Point(item.Position.X, item.Position.Y + 1), item.Steps + 1), item.Steps + 1);
            }

            if (open.Contains(hash[2]) && item.Position.X > 0)
            {
                queue.Enqueue((item.Passcode, $"{item.Directions}L", new Point(item.Position.X - 1, item.Position.Y), item.Steps + 1), item.Steps + 1);
            }

            if (open.Contains(hash[3]) && item.Position.X < 3)
            {
                queue.Enqueue((item.Passcode, $"{item.Directions}R", new Point(item.Position.X + 1, item.Position.Y), item.Steps + 1), item.Steps + 1);
            }
        }

        throw new PuzzleException("Solution not found.");
    }
}