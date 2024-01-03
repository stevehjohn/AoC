using AoC.Solutions.Exceptions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._02;

[UsedImplicitly]
public class Part2 : Base
{
    private readonly HashSet<string> _boxHashes = new();

    public override string GetAnswer()
    {
        var result = "Not found!";

        foreach (var line in Input)
        {
            result = AddHashes(line);

            if (result != null)
            {
                break;
            }
        }

        if (result == null)
        {
            throw new PuzzleException("Solution not found.");
        }

        return result.Replace("X", string.Empty);
    }

    private string AddHashes(string data)
    {
        for (var j = 0; j < data.Length; j++)
        {
            var line = $"{data[..j]}X{data[(j + 1)..]}";

            if (! _boxHashes.Add(line))
            {
                return line;
            }
        }

        return null;
    }
}