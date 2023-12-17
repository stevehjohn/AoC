using AoC.Solutions.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._03;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var houses = new Dictionary<Point, int> { { new(0, 0), 1 } };

        var position = new Point(0, 0);

        foreach (var c in Input[0])
        {
            switch (c)
            {
                case '^':
                    position = new Point(position.X, position.Y - 1);

                    break;
                case 'v':
                    position = new Point(position.X, position.Y + 1);

                    break;
                case '<':
                    position = new Point(position.X - 1, position.Y);

                    break;
                case '>':
                    position = new Point(position.X + 1, position.Y);

                    break;
            }

            if (! houses.TryAdd(position, 1))
            {
                houses[position]++;
            }
        }

        return houses.Count.ToString();
    }
}