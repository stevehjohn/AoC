using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._10;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var circle = new CircularLinkedList<int>();

        for (var i = 0; i < 256; i++)
        {
            circle.Add(i);
        }

        return "TESTING";
    }
}