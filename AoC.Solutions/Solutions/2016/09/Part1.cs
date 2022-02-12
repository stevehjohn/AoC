using System.Text;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._09;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var result = Decompress(Input[0]);

        return result.Length.ToString();
    }

    private static string Decompress(string input)
    {
        var decompressed = new StringBuilder();

        while (input.Length > 0)
        {
            if (input[0] == '(')
            {
                var parameters = input[1..input.IndexOf(')')].Split('x').Select(int.Parse).ToArray();

                input = input[(input.IndexOf(')') + 1)..];

                for (var i = 0; i < parameters[1]; i++)
                {
                    decompressed.Append(input[..parameters[0]]);
                }

                input = input[parameters[0]..];

                continue;
            }

            decompressed.Append(input[0]);

            input = input[1..];
        }

        return decompressed.ToString();
    }
}