using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2024._18;

public class PuzzleState
{
    public const int Size = 73;
    
    private static char[,] _map;

    public PuzzleState(string[] input, State endState)
    {
        if (_map == null)
        {
            _map = new char[Size, Size];
            
            for (var y = 0; y < Size; y++)
            {
                for (var x = 0; x < Size; x++)
                {
                    if (x is 0 or Size - 1 || y is 0 or Size - 1)
                    {
                        _map[x, y] = '#';
                        
                        continue;
                    }

                    _map[x, y] = '.';
                }
            }

            for (var i = 1_024; i < input.Length; i++)
            {
                var point = new Point2D(input[i]);

                _map[point.X, point.Y] = '#';
            }
        }
    }
}