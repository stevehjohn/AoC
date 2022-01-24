using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._23;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly byte[] _cups = new byte[10];

    private byte _cup;

    public override string GetAnswer()
    {
        ParseInput();

        for (var i = 0; i < 100; i++)
        {
            PerformMove();
        }

        return GetCupsFromOne();
    }

    private string GetCupsFromOne()
    {
        var cups = new char[8];

        var cup = 1;

        for (var i = 0; i < 8; i++)
        {
            cups[i] = (char) (_cups[cup] + '0');

            cup = _cups[cup];
        }

        return new string(cups);
    }

    private void PerformMove()
    {
        var targetCup = _cup;

        int i;

        var sequenceToShift = 0;

        var removed = new byte[3];

        for (i = 0; i < 4; i++)
        {
            targetCup = _cups[targetCup];

            if (i < 3)
            {
                removed[i] = targetCup;
            }

            if (i == 2)
            {
                sequenceToShift = targetCup;
            }
        }

        targetCup = (byte) (_cup - 1);

        if (targetCup == 0)
        {
            targetCup = 9;
        }

        while (targetCup == removed[0] || targetCup == removed[1] || targetCup == removed[2])
        {
            targetCup--;

            if (targetCup < 1)
            {
                targetCup = 9;
            }
        }

        var nextCup = _cups[_cup];

        _cups[_cup] = _cups[sequenceToShift];

        _cups[sequenceToShift] = _cups[targetCup];

        _cups[targetCup] = nextCup;

        _cup = _cups[_cup];
    }

    private void ParseInput()
    {
        var data = Input[0];

        for (var i = 0; i < data.Length - 1; i++)
        {
            _cups[(byte) (data[i] - '0')] = (byte) (data[i + 1] - '0');
        }

        _cups[(byte) (data[^1] - '0')] = (byte) (data[0] - '0');

        _cup = (byte) (Input[0][0] - '0');
    }
}