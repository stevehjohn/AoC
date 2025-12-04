using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._04;

[UsedImplicitly]
public class Part1 : Base
{
    private int _width;

    private int _height;
    
    private char[,] _map;
    
    public override string GetAnswer()
    {
        ParseInput();

        var sum = 0;
        
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                var surrounding = 0;

                if (_map[y, x] == '.')
                {
                    continue;
                }

                for (var y1 = -1; y1 < 2; y1++)
                {
                    for (var x1 = -1; x1 < 2; x1++)
                    {
                        if (y1 == 0 && x1 == 0)
                        {
                            continue;
                        }

                        if (SafeCheckCell(y + y1, x + x1) == '@')
                        {
                            surrounding++;
                        }
                    }
                }

                if (surrounding < 4)
                {
                    sum++;
                }
            }
        }

        return sum.ToString();
    }

    private char SafeCheckCell(int y, int x)
    {
        if (x < 0 || x >= _width || y < 0 || y >= _height)
        {
            return '\0';
        }

        return _map[y, x];
    }

    private void ParseInput()
    {
        _width = Input[0].Length;

        _height = Input.Length;
        
        _map = new char[_height, _width];

        for (var y = 0; y < _height; y++)
        {
            var line = Input[y];
            
            for (var x = 0; x < _width; x++)
            {
                _map[y, x] = line[x];
            }
        }
    }
}