using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._23;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly byte[] _cups = new byte[10];
    
    private int _cupLabel;

    public override string GetAnswer()
    {
        ParseInput();

        //for (var i = 0; i < 10; i++)
        {
            PerformMove();
        }

        return "TESTING";
    }

    private void PerformMove()
    {
        var targetCup = _cupLabel;

        int i;

        for (i = 0; i < 4; i++)
        {
            targetCup = _cups[targetCup];
        }

        if (targetCup != _cupLabel - 1)
        {
            i = 0;

            var highest = targetCup;

            var highestLower = 0;

            while (i < 4)
            {
                targetCup = _cups[targetCup];

                if (targetCup > highest)
                {
                    highest = targetCup;
                }

                if (targetCup < _cupLabel && targetCup > highestLower)
                {
                    highestLower = targetCup;
                }

                i++;
            }

            targetCup = highestLower == 0 ? highest : highestLower;
        }

        Console.WriteLine(targetCup);
    }

    private void ParseInput()
    {
        var data = Input[0];

        for (var i = 0; i < data.Length - 1; i++)
        {
            _cups[(byte)(data[i] - '0')] = (byte)(data[i + 1] - '0');
        }

        _cups[(byte)(data[^1] - '0')] = (byte)(data[0] - '0');

        _cupLabel = (byte)(Input[0][0] - '0');
    }
}