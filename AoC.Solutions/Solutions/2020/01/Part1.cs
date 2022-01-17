using AoC.Solutions.Exceptions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._01;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var integers = new int[Input.Length];

        for (var i = 0; i < Input.Length; i++)
        {
            integers[i] = int.Parse(Input[i]);
        }

        for (var i = 0; i < integers.Length; i++)
        {
            for (var j = i + 1; j < integers.Length; j++)
            {
                if (integers[i] + integers[j] == 2020)
                {
                    return (integers[i] * integers[j]).ToString();
                }
            }
        }

        throw new PuzzleException("Solution not found.");
    }
}