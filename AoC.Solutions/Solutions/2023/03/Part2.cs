using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._03;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        Width = Input[0].Length;

        Height = Input.Length;

        var gears = GetGears();

        var numbers = GetNumbers();
        
        return GetGearValues(gears, numbers).ToString();    
    }

    private int GetGearValues(List<(int X, int Y)> gears, List<(int X, int Y, int Number, int Length)> numbers)
    {
        var sum = 0;
        
        foreach (var gear in gears)
        {
            var neighbours = GetNeighbours(gear.X, gear.Y, numbers);

            if (neighbours.Count == 2)
            {
                sum += neighbours[0] * neighbours[1];
            }
        }

        return sum;
    }

    private List<int> GetNeighbours(int x, int y, List<(int X, int Y, int Number, int Length)> numbers)
    {
        var neighbours = new List<int>();
        
        foreach (var number in numbers)
        {
            if (y >= number.Y - 1 && y <= number.Y + 1)
            {
                if (x >= number.X - number.Length && x <= number.X + 1)
                {
                    neighbours.Add(number.Number);
                }
            }
        }

        return neighbours;
    }

    private List<(int X, int Y)> GetGears()
    {
        var gears = new List<(int X, int Y)>();
        
        for (var y = 0; y < Height; y++)
        {
            var line = Input[y];

            for (var x = 0; x < Width; x++)
            {
                if (line[x] == '*')
                {
                    gears.Add((x, y));
                }
            }
        }

        return gears;
    }

    private List<(int X, int Y, int Number, int Length)> GetNumbers()
    {
        var numbers = new List<(int X, int Y, int Number, int Length)>();
        
        for (var y = 0; y < Height; y++)
        {
            var line = Input[y];

            var number = 0;
            
            for (var x = 0; x < Width; x++)
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
                        numbers.Add((x - 1, y, number, number.ToString().Length));

                        number = 0;
                    }
                }
            }

            if (number != 0)
            {
                numbers.Add((Width - 1, y, number, number.ToString().Length));
            }
        }

        return numbers;
    }
}
