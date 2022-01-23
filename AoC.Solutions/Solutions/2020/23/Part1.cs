using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._23;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly byte[] _cupsIndexes = new byte[9];
    
    private readonly byte[] _cups = new byte[9];

    public override string GetAnswer()
    {
        ParseInput();

        for (var i = 0; i < 10; i++)
        {
            PerformMove();
        }

        return string.Concat(_cupsIndexes.Select(c => (char) (c + '0')));
    }

    private void PerformMove()
    {
    }

    private void ParseInput()
    {
        var i = 0;

        foreach (var c in Input[0])
        {
            _cupsIndexes[(byte) (c - '1')] = (byte) i;

            _cups[i] = (byte) (c - '0');

            i++;
        }
    }
}