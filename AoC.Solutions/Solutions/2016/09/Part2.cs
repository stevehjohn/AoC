using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._09;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var result = GetDecompressedLength(Input[0], 0, Input[0].Length);

        return result.ToString();
    }

    private static long GetDecompressedLength(string input, int startIndex, int length)
    {
        var result = 0L;

        for (var i = startIndex; i < startIndex + length; i++)
        {
            if (input[i] == '(')
            {
                var parameters = input[(i + 1)..input.IndexOf(')', i)].Split('x').Select(int.Parse).ToArray();

                i = input.IndexOf(')', i) + 1;

                result += GetDecompressedLength(input, i, parameters[0]) * parameters[1];

                i += parameters[0] - 1;

                continue;
            }

            result++;
        }

        return result;
    }
}