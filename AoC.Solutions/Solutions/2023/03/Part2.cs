using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._03;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var sum = 0;

        Width = Input[0].Length;

        Height = Input.Length;
        
        for (var y = 0; y < Height; y++)
        {
            var line = Input[y];

            for (var x = 0; x < Width; x++)
            {
                if (line[x] == '*')
                {
                    sum += CheckGear(x, y);
                }
            }
        }

        return sum.ToString();    
    }

    private int CheckGear(int oX, int oY)
    {
        var numbers = new List<int>();

        for (var y = oY - 1; y < oY + 2; y++)
        {
            var line = Input[y];

            var number = 0;

            for (var x = oX - 3; x < oX + 4; x++)
            {
                if (char.IsNumber(line[x]))
                {
                    number *= 10;

                    number += line[x] - '0';
                }
                else
                {
                    if (number != 0)
                    {
                        numbers.Add(number);

                        number = 0;
                    }
                }
            }

            if (number != 0)
            {
                numbers.Add(number);
            }
        }

        if (numbers.Count == 2)
        {
            return numbers[0] * numbers[1];
        }

        return 0;
    }
}
