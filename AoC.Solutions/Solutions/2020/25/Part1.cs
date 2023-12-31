using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._25;

[UsedImplicitly]
public class Part1 : Base
{
    private long _pk1;

    private long _pk2;

    public override string GetAnswer()
    {
        ParseInput();

        long smallerPkLoopSize;

        long result;

        if (_pk1 < _pk2)
        {
            smallerPkLoopSize = GetLoopSize(_pk1);

            result = Transform(_pk2, smallerPkLoopSize);
        }
        else
        {
            smallerPkLoopSize = GetLoopSize(_pk2);

            result = Transform(_pk1, smallerPkLoopSize);
        }

        return result.ToString();
    }

    private static long Transform(long key, long loopSize)
    {
        var value = 1L;

        for (var i = 0; i < loopSize; i++)
        {
            value *= key;

            value %= 20201227;
        }

        return value;
    }

    private static long GetLoopSize(long key)
    {
        var value = 1L;

        long loop = 0;

        while (value != key)
        {
            value *= 7;

            value %= 20201227;

            loop++;
        }

        return loop;
    }

    private void ParseInput()
    {
        _pk1 = long.Parse(Input[0]);

        _pk2 = long.Parse(Input[1]);
    }
}