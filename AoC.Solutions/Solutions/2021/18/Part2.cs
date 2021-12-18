using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._18;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var largest = 0;

        for (var outer = 0; outer < Input.Length; outer++)
        {
            for (var inner = 0; inner < Input.Length; inner++)
            {
                if (inner == outer)
                {
                    continue;
                }

                var added = Add(Number.Parse(Input[outer]), Number.Parse(Input[inner]));

                var magnitude = GetMagnitude(added);

                if (magnitude > largest)
                {
                    largest = magnitude;
                }
            }
        }

        return largest.ToString();
    }
}