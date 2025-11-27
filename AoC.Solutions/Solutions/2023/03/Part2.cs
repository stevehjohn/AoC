using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._03;

[UsedImplicitly]
public class Part2 : Base
{
    private List<int> _neighbours;

    private Dictionary<int, List<(int X, int Number, int Length)>> _numbers;
    
    public override string GetAnswer()
    {
        _neighbours = [];

        _numbers = new Dictionary<int, List<(int X, int Number, int Length)>>();
        
        Width = Input[0].Length;

        Height = Input.Length;

        var gears = GetGears();

        GetNumbers();
        
        return GetGearValues(gears).ToString();    
    }

    private int GetGearValues(List<(int X, int Y)> gears)
    {
        var sum = 0;
        
        foreach (var gear in gears)
        {
            GetNeighbours(gear.X, gear.Y);

            if (_neighbours.Count == 2)
            {
                sum += _neighbours[0] * _neighbours[1];
            }
        }

        return sum;
    }

    private void GetNeighbours(int x, int y)
    {
        _neighbours.Clear();

        for (var checkY = y - 1; checkY < y + 2; checkY++)
        {
            foreach (var number in _numbers[checkY])
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

    private void GetNumbers()
    {
        for (var y = 0; y < Height; y++)
        {
            _numbers.Add(y, []);
        }

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
                        _numbers[y].Add((x - 1, number, number.ToString().Length));

                        number = 0;
                    }
                }
            }

            if (number != 0)
            {
                _numbers[y].Add((Width - 1, number, number.ToString().Length));
            }
        }
    }
}
