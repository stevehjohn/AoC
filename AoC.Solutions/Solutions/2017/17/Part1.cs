using AoC.Solutions.Extensions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._17;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var steps = int.Parse(Input[0]);

        var circle = new LinkedList<int>();

        circle.AddFirst(0);

        var current = circle.First;

        for (var number = 1; number < 2018; number++)
        {

            for (var i = 0; i < steps; i++)
            {
                current = current.NextCircular();
            }

            // ReSharper disable once AssignNullToNotNullAttribute
            circle.AddAfter(current, number);

            current = current.Next;
        }

        // ReSharper disable once PossibleNullReferenceException
        return current.Next?.Value.ToString();
    }
}