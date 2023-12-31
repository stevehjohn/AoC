using AoC.Solutions.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._03;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var houses = new Dictionary<Point, int> { { new(0, 0), 1 } };

        var santa = new Point(0, 0);
        
        var roboSanta = new Point(0, 0);

        var santaTurn = true;

        foreach (var c in Input[0])
        {
            Point position;

            if (santaTurn)
            {
                position = santa;
            }
            else
            {
                position = roboSanta;
            }

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

            if (santaTurn)
            {
                santa = position;
            }
            else
            {
                roboSanta = position;
            }

            if (! houses.TryAdd(position, 1))
            {
                houses[position]++;
            }

            santaTurn = ! santaTurn;
        }

        return houses.Count.ToString();
    }
}