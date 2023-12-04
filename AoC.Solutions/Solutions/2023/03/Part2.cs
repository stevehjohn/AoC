using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._03;

[UsedImplicitly]
public class Part2 : Base
{
    private readonly List<int> _neighbours = new();
    
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
            GetNeighbours(gear.X, gear.Y, numbers);

            if (_neighbours.Count == 2)
            {
                sum += _neighbours[0] * _neighbours[1];
            }
        }

        return sum;
    }

    private void GetNeighbours(int x, int y, List<(int X, int Y, int Number, int Length)> numbers)
    {
        _neighbours.Clear();
        
        foreach (var number in numbers)
        {
            if (y >= number.Y - 1 && y <= number.Y + 1)
            {
                if (x >= number.X - number.Length && x <= number.X + 1)
                {
                    if (_neighbours.Count == 2)
                    {
                        _neighbours.Clear();
                        
                        return;
                    }

                    _neighbours.Add(number.Number);
                }
            }
        }
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
