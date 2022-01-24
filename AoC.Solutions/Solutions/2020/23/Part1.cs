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

        for (var i = 0; i < 10; i++)
        {
            PerformMove();

            Console.WriteLine(GetCupsFromOne());
        }

        return "TESTING";
    }

    private string GetCupsFromOne()
    {
        var cups = new char[9];

        var cup = 1;

        for (var i = 0; i < 9; i++)
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

        for (i = 0; i < 4; i++)
        {
            targetCup = _cups[targetCup];

            if (i == 2)
            {
                sequenceToShift = targetCup;
            }
        }

        if (targetCup != _cup - 1)
        {
            i = 0;

            var highest = targetCup;

            byte highestLower = 0;

            while (i < 4)
            {
                targetCup = _cups[targetCup];

                if (targetCup > highest)
                {
                    highest = targetCup;
                }

                if (targetCup < _cup && targetCup > highestLower)
                {
                    highestLower = targetCup;
                }

                i++;
            }

            targetCup = highestLower == 0 ? highest : highestLower;
        }

        // Remember what was right of current cup 'r'
        var nextCup = _cups[_cup];

        // Shift everything after current cup << 3

        /*
            (3) 8  9  1  2  5  4  6  7
            Update 3 with what 1 points at
        */

        _cups[_cup] = _cups[sequenceToShift];

        // Find target
        _cups[sequenceToShift] = _cups[targetCup];

        // Point target ar 'r'
        _cups[targetCup] = nextCup;

        _cup = _cups[_cup];

        // How to determine what 1 should point at?
        // 1 points at cup after target

        //for (i = 1; i < 10; i++)
        //{
        //    if (_cups[i] == targetCup)
        //    {
        //        _cups[i] = nextCup;

        //        break;
        //    }
        //}

        //_cups[targetCup] = _cups[_cup]; // move 8 to after 2

        //_cups[_cup] = targetCup; // move 2 to after 3

        //_cup = targetCup;
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