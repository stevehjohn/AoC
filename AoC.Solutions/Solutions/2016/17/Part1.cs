using AoC.Solutions.Common;
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

        return result.ToString();
    }

    private static int Solve(string passcode)
    {
        var queue = new PriorityQueue<(string Passcode, Point Position, int Steps), int>();

        queue.Enqueue((passcode, new Point(0, 0), 0), 0);

        var open = new[] { 'B', 'C', 'D', 'E', 'F' };

        while (queue.Count > 0)
        {
            var item = queue.Dequeue();

            if (item.Position.X == 3 && item.Position.Y == 3)
            {
                Console.WriteLine(item.Passcode);
                return item.Steps;
            }

            var hash = Convert.ToHexString(MD5.HashData(Encoding.ASCII.GetBytes(item.Passcode)));

            if (open.Contains(hash[0]) && item.Position.Y > 0)
            {
                queue.Enqueue(($"{item.Passcode}U", new Point(item.Position.X, item.Position.Y - 1), item.Steps + 1), item.Steps + 1);
            }

            if (open.Contains(hash[1]) && item.Position.Y < 3)
            {
                queue.Enqueue(($"{item.Passcode}D", new Point(item.Position.X, item.Position.Y + 1), item.Steps + 1), item.Steps + 1);
            }

            if (open.Contains(hash[2]) && item.Position.X > 0)
            {
                queue.Enqueue(($"{item.Passcode}L", new Point(item.Position.X - 1, item.Position.Y), item.Steps + 1), item.Steps + 1);
            }

            if (open.Contains(hash[3]) && item.Position.X < 3)
            {
                queue.Enqueue(($"{item.Passcode}R", new Point(item.Position.X + 1, item.Position.Y), item.Steps + 1), item.Steps + 1);
            }
        }

        return 0;
    }
}