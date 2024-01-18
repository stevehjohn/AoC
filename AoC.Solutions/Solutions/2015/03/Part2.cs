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
            var position = santaTurn ? santa : roboSanta;

            position = c switch
            {
                '^' => new Point(position.X, position.Y - 1),
                'v' => new Point(position.X, position.Y + 1),
                '<' => new Point(position.X - 1, position.Y),
                '>' => new Point(position.X + 1, position.Y),
                _ => position
            };

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